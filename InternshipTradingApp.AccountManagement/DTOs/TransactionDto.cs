
namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Bank { get; set; } = string.Empty;
    }
}
