using System;
using Serilog;
using VRTradingService;

namespace vr.mock.api.Services
{
    /// <inheritdoc />
    public class StrategyTradingService : IStrategyTradingService
    {
        private readonly IVRTradingService _tradingService;

        public StrategyTradingService(IVRTradingService tradingService)
        {
            _tradingService = tradingService;
        }

        public decimal? GetLiveQuote(string ticker)
        {
            decimal? quote = null;
            try
            {
                quote = this._tradingService.GetQuote(ticker);
            }
            catch (QuoteException ex)
            {
                Log.Error(ex, $"GetLiveQuote failed for ticker: {ex.Ticker}");
            }

            return quote;
        }

        public void SellStrategy(string ticker, int quantity)
        {
            try
            {
                this._tradingService.Sell(ticker, quantity);
            }
            catch (TradeException ex)
            {
                Log.Error(ex, $"Sell trade failed for ticker: {ex.Ticker}");
            }
        }

        public void BuyStrategy(string ticker, int quantity)
        {
            try
            {
                this._tradingService.Buy(ticker, quantity);
            }
            catch (TradeException ex)
            {
                Log.Error(ex, $"Buy trade failed for ticker: {ex.Ticker}");
            }
        }
    }
}