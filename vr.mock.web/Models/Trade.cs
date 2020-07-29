namespace vr.mock.web.Models
{
    public class Trade   
    {
        public string Ticker { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public decimal Profit => SellPrice - BuyPrice;
    }
}