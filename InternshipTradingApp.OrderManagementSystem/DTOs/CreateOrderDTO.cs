using InternshipTradingApp.OrderManagementSystem.Entities;

namespace InternshipTradingApp.OrderManagementSystem.DTOs
{
    public class CreateOrderDTO
    {
        public int CustomerId { get; set; }
        public string StockSymbol { get; set; } = string.Empty;
        public int Quantity { get; set; }         
        public decimal Price { get; set; }        
        public OrderType Type { get; set; }   
    }
}
