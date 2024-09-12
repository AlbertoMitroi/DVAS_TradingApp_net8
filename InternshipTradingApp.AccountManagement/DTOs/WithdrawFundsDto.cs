using System.ComponentModel.DataAnnotations;

namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class WithdrawFundsDto
    {
        [Required]
        public int BankAccountId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}
