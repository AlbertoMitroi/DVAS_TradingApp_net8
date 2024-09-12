using System.ComponentModel.DataAnnotations;

namespace InternshipTradingApp.AccountManagement.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive amount.")]
        public decimal Balance { get; set; }

        [Required]
        public string IBAN { get; set; } = string.Empty;

        [Required]
        public string BankName { get; set; } = string.Empty;

        [Required]
        public AppUser User { get; set; } = new AppUser();
    }
}
