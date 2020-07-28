using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using vr.mock.api.Dtos;
using vr.mock.api.Services;

namespace vr.mock.api.Controllers
{
    [Route("api/[controller]")]
    public class StrategyController : ControllerBase
    {
        private readonly IHostedServiceAccessor<IStrategyManagementService> _strategyManagementService;
        private readonly ILogger<StrategyController> _logger;

        public StrategyController(IHostedServiceAccessor<IStrategyManagementService> strategyManagementService, ILogger<StrategyController> logger)
        {
            _strategyManagementService = strategyManagementService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation(nameof(RegisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(string))]
        public IActionResult RegisterStrategy(StrategyDetailsDto strategyDetails)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(UnregisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult UnregisterStrategy(string id)
        {
            throw new NotImplementedException();
        }
    }
}
