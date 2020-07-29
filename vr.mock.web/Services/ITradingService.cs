using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vr.mock.web.Models;

namespace vr.mock.web.Services
{
    /// <summary>
    /// A service to fetch the trading data and build trade view model
    /// </summary>
    public interface ITradingService
    {
        Task<TradeViewModel> GetTradeViewModel();
    }
}
