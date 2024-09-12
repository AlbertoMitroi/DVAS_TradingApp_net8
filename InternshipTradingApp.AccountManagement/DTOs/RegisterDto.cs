using System.ComponentModel.DataAnnotations;

namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters.")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
