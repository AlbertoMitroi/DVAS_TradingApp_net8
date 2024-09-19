
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipTradingApp.OrderManagementSystem.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string StockSymbol { get; set; }  = string.Empty;
        [Column(TypeName = "decimal(18,6)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }        
        public OrderType Type { get; set; }       
        public OrderStatus Status { get; set; }   
        public DateTime OrderDate { get; set; }
    }

    public enum OrderType
    {
        Buy,
        Sell
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Canceled,
        Failed
    }
}
