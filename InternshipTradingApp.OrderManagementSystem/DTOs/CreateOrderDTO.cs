using InternshipTradingApp.OrderManagementSystem.Entities;

namespace InternshipTradingApp.OrderManagementSystem.DTOs
{
    public class CreateOrderDTO
    {
        public int CustomerId { get;  private set; }
        public string StockSymbol { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderType Type { get; set; }


        public void SetId(int id) => CustomerId = id;
    }
}
