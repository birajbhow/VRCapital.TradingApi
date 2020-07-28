using Microsoft.Extensions.Hosting;
using vr.mock.api.Dtos;

namespace vr.mock.api.Services
{
    public interface IStrategyManagementService : IHostedService
    {
        string RegisterStrategy(StrategyDetailsDto strategyDetails);
        bool UnregisterStrategy(string strategyId);
    }
}