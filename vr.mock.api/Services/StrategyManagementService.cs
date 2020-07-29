using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;
using VRTradingInfrastructureServices;
using VRTradingService;

namespace vr.mock.api.Services
{
    internal class StrategyManagementService : VRBackgroundServiceBase, IStrategyManagementService
    {
        private readonly IVRTradingService _tradingService;
        private readonly List<Strategy> _strategies;
        private const int TickFrequencyMilliseconds = 1000;

        public StrategyManagementService(ILogger<StrategyManagementService> logger,
            IVRTradingService tradingService) 
            : base(TimeSpan.FromMilliseconds(TickFrequencyMilliseconds), logger)
        {
            _tradingService = tradingService;
            _strategies = new List<Strategy>();
        }

        protected override Task CheckStrategies()
        {
            foreach (var runningStrategy in this._strategies)
            {
                if (runningStrategy.ExecutionPrice > 0) continue;
                
                var liveQuote = this.GetLiveQuote(runningStrategy.Ticker); 
                if (liveQuote == null) continue;
                
                if (runningStrategy.Instruction == BuySell.Buy)
                {
                    this.ExecuteBuyStrategy(runningStrategy, liveQuote.Value);
                }
                else if (runningStrategy.Instruction == BuySell.Sell)
                {
                    this.ExecuteSellStrategy(runningStrategy, liveQuote.Value);
                }
            }

            return Task.CompletedTask;
        }

        public string RegisterStrategy(StrategyDetailsDto strategyDetails)
        {
            var liveQuote = this.GetLiveQuote(strategyDetails.Ticker);
            if (liveQuote != null)
            {
                var strategyId = Guid.NewGuid().ToString();
                this._strategies.Add(new Strategy
                {
                    Id = strategyId,
                    Instruction = strategyDetails.Instruction,
                    PriceMovement = strategyDetails.PriceMovement,
                    Quantity = strategyDetails.Quantity,
                    Ticker = strategyDetails.Ticker,
                    StartPrice = liveQuote.Value
                });
                return strategyId;
            }

            return null;
        }

        public bool UnregisterStrategy(string strategyId)
        {
            return this._strategies.Remove(this._strategies.Find(s => s.Id == strategyId));
        }

        public List<ExecutedStrategyDto> GetExecutedStrategies()
        {
            return this._strategies
                .Where(s => s.ExecutionPrice > 0)
                .Select(s => new ExecutedStrategyDto
                {
                    Ticker = s.Ticker,
                    Instruction = s.Instruction,
                    ExecutionPrice = s.ExecutionPrice
                })
                .ToList();
        }

        private decimal? GetLiveQuote(string ticker)
        {
            decimal? quote = null;
            try
            {
                quote = this._tradingService.GetQuote(ticker);
            }
            catch (QuoteException ex)
            {
                Console.WriteLine(ex);
            }

            return quote;
        }

        private void ExecuteSellStrategy(Strategy strategy, decimal liveQuote)
        {
            try
            {
                var desiredPrice = ((100 + strategy.PriceMovement) / 100) * strategy.StartPrice;

                if (liveQuote >= desiredPrice)
                {
                    this._tradingService.Sell(strategy.Ticker, strategy.Quantity);
                    strategy.ExecutionPrice = liveQuote;
                }
            }
            catch (TradeException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ExecuteBuyStrategy(Strategy strategy, decimal liveQuote)
        {
            try
            {
                var desiredPrice = ((100 - strategy.PriceMovement) / 100) * strategy.StartPrice;

                if (liveQuote <= desiredPrice)
                {
                    this._tradingService.Buy(strategy.Ticker, strategy.Quantity);
                    strategy.ExecutionPrice = liveQuote;
                }
            }
            catch (TradeException ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
