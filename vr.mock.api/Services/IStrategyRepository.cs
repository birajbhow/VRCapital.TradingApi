using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vr.mock.api.DomainObjects;
using vr.mock.api.Dtos;

namespace vr.mock.api.Services
{
    /// <summary>
    /// A repository service to add, remove and fetch trade strategies
    /// </summary>
    public interface IStrategyRepository
    {
        /// <summary>
        /// Register new strategy
        /// </summary>
        /// <param name="strategyDetails"></param>
        /// <returns>Registered Strategy Id</returns>
        string RegisterStrategy(StrategyDetailsDto strategyDetails);

        /// <summary>
        /// Un-register a strategy by id
        /// </summary>
        /// <param name="strategyId"></param>
        /// <returns>Un-registered Strategy Id</returns>
        bool UnregisterStrategy(string strategyId);

        /// <summary>
        /// Retrieve all executed strategies
        /// </summary>
        /// <returns></returns>
        List<ExecutedStrategyDto> GetExecutedStrategies();
    }
}
