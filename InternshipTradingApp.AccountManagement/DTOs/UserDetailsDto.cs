namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class UserDetailsDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required decimal Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
