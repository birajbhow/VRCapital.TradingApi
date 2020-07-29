using System.ComponentModel.DataAnnotations;
using VRTradingInfrastructureServices;

namespace vr.mock.api.Dtos
{
    public class StrategyDetailsDto
    {
        [Required]
        public string Ticker { get; set; }
        
        [Required]
        public BuySell Instruction { get; set; }
        
        [Required]
        public decimal PriceMovement { get; set; }
        
        [Required]
        public int Quantity { get; set; }
    }
}