using System;
using VRTradingInfrastructureServices;

namespace vr.mock.api.DomainObjects
{
    public class Strategy
    {
        public string Id { get; set; }
        
        public string Ticker { get; set; }
        
        public BuySell Instruction { get; set; }
        
        public decimal PriceMovement { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal StartPrice { get; set; }

        public decimal ExecutionPrice { get; set; } = 0;
    }
}