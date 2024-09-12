using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace InternshipTradingApp.AccountManagement.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public decimal Balance { get; set; } = 0;
        public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
