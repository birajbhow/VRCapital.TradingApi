using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;

namespace vr.mock.api.Services
{
    public interface IStrategyRepository
    {
        string RegisterStrategy(StrategyDetailsDto strategyDetails);

        bool UnregisterStrategy(string strategyId);

        List<ExecutedStrategyDto> GetExecutedStrategies();
    }
}
