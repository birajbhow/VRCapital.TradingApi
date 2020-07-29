using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vr.mock.web.Models;

namespace vr.mock.web.Services
{
    public interface ITradingService
    {
        Task<TradeViewModel> GetTradeViewModel();
    }
}
