using System.Text.Json;
using InternshipTradingApp.AccountManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.AccountManagement.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSeedData.json");
            var userData = await File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var userSeedRoot = JsonSerializer.Deserialize<UserSeedRoot>(userData, options);

            if (userSeedRoot == null || userSeedRoot.Users == null) return;

            var roles = new List<AppRole>
            {
                new() { Name = "Member" },
                new() { Name = "Admin" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var userDto in userSeedRoot.Users)
            {
                var user = new AppUser
                {
                    UserName = NormalizeUserName(userDto.FullName),
                    Email = userDto.Email,
                    Balance = userDto.Balance,
                    BankAccounts = new List<Entities.BankAccount>()
                };

                foreach (var ba in userDto.BankAccounts)
                {
                    var bankAccount = new Entities.BankAccount
                    {
                        IBAN = ba.IBAN,
                        BankName = ba.BankName,
                        User = user
                    };

                    user.BankAccounts.Add(bankAccount);
                }

                var result = await userManager.CreateAsync(user, "Password@1");
                if (!result.Succeeded) continue;

                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@domain.com",
                Balance = 0
            };

            await userManager.CreateAsync(admin, "Password@1");
            await userManager.AddToRolesAsync(admin, new[] { "Admin" });
        }


        private static string NormalizeUserName(string fullName)
        {
            return fullName.Trim().Replace(" ", "_").ToLower();
        }
    }
    public class UserSeedDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Balance { get; set; }
        public List<BankAccountSeed> BankAccounts { get; set; } = [];
    }

    public class BankAccountSeed
    {
        public string IBAN { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
    public class UserSeedRoot
    {
        public List<UserSeedDto> Users { get; set; } = new();
    }
}
