using System;
using System.Collections.Generic;
using System.Linq;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;

namespace vr.mock.api.Services
{
    /// <inheritdoc />
    public class StrategyRepository : IStrategyRepository
    {
        private readonly IStrategyTradingService _strategyTradingService;
        private readonly ILocalCache _localCache;
        private const string StrategiesCacheKey = "_strategies";

        public StrategyRepository(IStrategyTradingService strategyTradingService,
            ILocalCache localCache)
        {
            _strategyTradingService = strategyTradingService;
            _localCache = localCache;
        }

        /// <inheritdoc />
        public string RegisterStrategy(StrategyDetailsDto strategyDetails)
        {
            var liveQuote = this._strategyTradingService.GetLiveQuote(strategyDetails.Ticker);
            if (liveQuote != null)
            {
                var strategyId = Guid.NewGuid().ToString();
                this._localCache.Put(strategyId, new Strategy
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

        /// <inheritdoc />
        public bool UnregisterStrategy(string strategyId)
        {
            return this._localCache.Remove(strategyId);
        }

        /// <inheritdoc />
        public List<ExecutedStrategyDto> GetExecutedStrategies()
        {
            var strategies = this._localCache.GetAll<Strategy>();
            return strategies
                .Where(s => s.ExecutionPrice > 0)
                .Select(s => new ExecutedStrategyDto
                {
                    Ticker = s.Ticker,
                    Instruction = s.Instruction,
                    ExecutionPrice = s.ExecutionPrice
                })
                .ToList();
        }
    }
}