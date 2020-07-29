using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vr.mock.api.Services
{
    public interface IStrategyTradingService
    {
        decimal? GetLiveQuote(string ticker);

        void SellStrategy(string ticker, int quantity);

        void BuyStrategy(string ticker, int quantity);
    }
}
