using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;
using VRTradingInfrastructureServices;
using VRTradingService;

namespace vr.mock.api.Services
{
    internal class StrategyManagementService : VRBackgroundServiceBase, IStrategyManagementService
    {
        private readonly IStrategyTradingService _strategyTradingService;
        private readonly ILocalCache _localCache;


        private const int TickFrequencyMilliseconds = 1000;
        

        public StrategyManagementService(ILogger<StrategyManagementService> logger,
            IStrategyTradingService strategyTradingService,
            ILocalCache localCache) 
            : base(TimeSpan.FromMilliseconds(TickFrequencyMilliseconds), logger)
        {
            _strategyTradingService = strategyTradingService;
            _localCache = localCache;
        }

        protected override Task CheckStrategies()
        {
            // get all non executed strategies
            var strategies = this._localCache
                .GetAll<Strategy>()
                .Where(s => s.ExecutionPrice == 0);

            foreach (var runningStrategy in strategies)
            {   
                var liveQuote = this._strategyTradingService.GetLiveQuote(runningStrategy.Ticker); 
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

        #region Private Methods

        private void ExecuteBuyStrategy(Strategy strategy, decimal liveQuote)
        {
            try
            {
                // calculating desired buy price based on starting price and price movement
                var desiredPrice = ((100 - strategy.PriceMovement) / 100) * strategy.StartPrice;

                if (liveQuote <= desiredPrice)
                {
                    this._strategyTradingService.BuyStrategy(strategy.Ticker, strategy.Quantity);
                    strategy.ExecutionPrice = liveQuote;
                    this._localCache.Put(strategy.Id, strategy);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ExecuteBuyStrategy() method exception");
            }
        }

        private void ExecuteSellStrategy(Strategy strategy, decimal liveQuote)
        {
            try
            {
                // calculating desired sell price based on starting price and price movement
                var desiredPrice = ((100 + strategy.PriceMovement) / 100) * strategy.StartPrice;

                if (liveQuote >= desiredPrice)
                {
                    this._strategyTradingService.SellStrategy(strategy.Ticker, strategy.Quantity);
                    strategy.ExecutionPrice = liveQuote;
                    this._localCache.Put(strategy.Id, strategy);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ExecuteSellStrategy() method exception");
            }
        }

        #endregion
    }
}
