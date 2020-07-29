using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRTradingInfrastructureServices;

namespace vr.mock.web.Models
{
    public class ExecutedStrategy
    {
        public string Ticker { get; set; }

        public BuySell Instruction { get; set; }

        public decimal ExecutionPrice { get; set; }
    }
}
