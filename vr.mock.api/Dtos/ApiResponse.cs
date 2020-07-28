using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vr.mock.api.Dtos
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string Message => Success ? "Execution Successful!" : "Execution Failed!";

        public string StrategyId { get; set; }
    }
}
