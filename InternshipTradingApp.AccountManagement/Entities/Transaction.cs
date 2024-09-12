using System.ComponentModel.DataAnnotations;

namespace InternshipTradingApp.AccountManagement.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        [Required]
        public AppUser User { get; set; } = new AppUser();

        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; } = new BankAccount();
    }
}
