using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vr.mock.api.Services
{
    /// <summary>
    /// A wrapper service on VRTrading service to manage all strategy trading and exceptions
    /// </summary>
    public interface IStrategyTradingService
    {
        decimal? GetLiveQuote(string ticker);

        void SellStrategy(string ticker, int quantity);

        void BuyStrategy(string ticker, int quantity);
    }
}
