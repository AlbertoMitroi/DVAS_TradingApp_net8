using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.AccountManagement.Services
{
    public class FundsService(UserManager<AppUser> userManager, AccountDbContext context, IUserNotificationService userNotification) : IFundsService
    {
        public async Task AddFundsAsync(int userId, decimal amount)
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId) 
                ?? throw new Exception("User not found");

            user.Balance += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                Date = DateTime.UtcNow,
                Type = "Deposit",
                User = user,
                UserId = userId,
                BankAccountId = 0
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            if (userId.ToString() == null) throw new Exception("UserId is null");
            await userNotification.SendUserDetailsAsync(userId.ToString());
        }

        public async Task WithdrawFundsAsync(int userId, int bankId, decimal amount)
        {
            var user = await userManager.Users
                .Include(u => u.BankAccounts)
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new Exception("User not found");

            if (user.Balance < amount)
                throw new Exception("Insufficient funds");

            var bankAccount = user.BankAccounts
                .FirstOrDefault(ba => ba.Id == bankId)
                ?? throw new Exception("Bank Account not found");

            user.Balance -= amount;

            var transaction = new Transaction
            {
                Amount = -amount,
                Date = DateTime.UtcNow,
                Type = "Withdraw",
                User = user,
                UserId = userId,
                BankAccount = bankAccount,
                BankAccountId = bankId
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            if (userId.ToString() == null) throw new Exception("UserId is null");
            await userNotification.SendUserDetailsAsync(userId.ToString());
        }
    }
}
