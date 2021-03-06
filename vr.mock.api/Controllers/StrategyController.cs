﻿using System;
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
        private readonly IStrategyRepository _strategyRepository;
        
        public StrategyController(IStrategyRepository strategyRepository)
        {
            _strategyRepository = strategyRepository;
        }

        [HttpPost]
        [SwaggerOperation(nameof(RegisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK", typeof(string))]
        public IActionResult RegisterStrategy(StrategyDetailsDto strategyDetails)
        {
            var result = this._strategyRepository.RegisterStrategy(strategyDetails);
            return new OkObjectResult(new ApiResponse
            {
                Success = result != null,
                StrategyId = result
            });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(UnregisterStrategy))]
        [SwaggerResponse(StatusCodes.Status200OK, "OK")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Not Found")]
        public IActionResult UnregisterStrategy(string id)
        {
            var result = this._strategyRepository.UnregisterStrategy(id);
            
            return new OkObjectResult(new ApiResponse
            {
                Success = result,
                StrategyId = id
            });
        }

        [HttpGet]
        [SwaggerOperation(nameof(GetExecutedStrategies))]
        public IActionResult GetExecutedStrategies()
        {
            return new OkObjectResult(this._strategyRepository.GetExecutedStrategies());
        }
    }
}
