using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VRTradingInfrastructureServices;

namespace vr.mock.api.Services
{
    internal class StrategyManagementService : VRBackgroundServiceBase, IStrategyManagementService
    {
        private const int TickFrequencyMilliseconds = 1000;

        public StrategyManagementService(ILogger<StrategyManagementService> logger) : base(TimeSpan.FromMilliseconds(TickFrequencyMilliseconds), logger)
        {
            //Your code Here
        }

        protected override Task CheckStrategies()
        {
            // TODO: Check registered strategies.
            //Your code here
            return Task.CompletedTask;
        }
    }
}
