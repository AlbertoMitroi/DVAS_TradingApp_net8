using System.ComponentModel.DataAnnotations;

namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class BankAccountDto
    {
        [Required]
        public string IBAN { get; set; } = string.Empty;

        [Required]
        public string BankName { get; set; } = string.Empty;
    }
}
