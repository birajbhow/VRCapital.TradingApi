using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;

namespace vr.mock.api.Services
{
    public interface IStrategyManagementService : IHostedService
    {
        string RegisterStrategy(StrategyDetailsDto strategyDetails);

        bool UnregisterStrategy(string strategyId);
        
        List<ExecutedStrategyDto> GetExecutedStrategies();
    }
}