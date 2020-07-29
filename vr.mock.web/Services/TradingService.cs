using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using vr.mock.web.Models;
using VRTradingInfrastructureServices;

namespace vr.mock.web.Services
{
    public class TradingService : ITradingService
    {
        private readonly IConfiguration _configuration;

        public TradingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TradeViewModel> GetTradeViewModel()
        {
            // get strategies from trading api
            var executedStrategies = await this.GetStrategies();
            var trades = new List<Trade>();
            var strategiesByTicker = executedStrategies
                .GroupBy(s => s.Ticker);

            foreach (var strategy in strategiesByTicker)
            {
                var buyStrategy = strategy.FirstOrDefault(s => s.Instruction == BuySell.Buy);
                var sellStrategy = strategy.FirstOrDefault(s => s.Instruction == BuySell.Sell);

                if (buyStrategy != null && sellStrategy != null)
                {
                    trades.Add(new Trade()
                    {
                        Ticker = strategy.Key,
                        BuyPrice = buyStrategy.ExecutionPrice,
                        SellPrice = sellStrategy.ExecutionPrice
                    });
                }
            }

            return new TradeViewModel() { Trades = trades};
        }

        private async Task<List<ExecutedStrategy>> GetStrategies()
        {
            var url = this._configuration["TradingApi:Url"];
            return await url
                .AppendPathSegments("api", "strategy")
                .WithHeader("cache-control", "no-cache")
                .GetJsonAsync<List<ExecutedStrategy>>();
        }
    }
}