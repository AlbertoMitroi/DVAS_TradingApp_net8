using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using InternshipTradingApp.Server.SignalR;

namespace InternshipTradingApp.AccountManagement.Services
{
    public class UserNotificationService(
        UserManager<AppUser> userManager,
        AccountDbContext dbContext,
        IHubContext<UserNotificationHub> hubContext,
        ILogger<UserNotificationService> logger) : IUserNotificationService
    {
        public async Task SendUserDetailsAsync(string userId)
        {
            logger.LogInformation($"Fetching user details for user ID: {userId}");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogError("User not found.");
                throw new Exception("User not found.");
            }

            var transactions = await dbContext.Transactions
                .Include(t => t.BankAccount)
                .Where(t => t.UserId == int.Parse(userId))
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Date = t.Date,
                    Type = t.Type,
                    Bank = t.Type == "Deposit" ?
                          "Stripe Card"
                        : (t.BankAccount != null ? t.BankAccount.BankName : "Unknown")
                })
                .ToListAsync();
            if (user.UserName == null || user.Email == null) throw new Exception("Username or Email is null");

            var userDetails = new UserDetailsDto
            {
                Username = user.UserName,
                Email = user.Email,
                Balance = user.Balance,
                Transactions = transactions
            };

            logger.LogInformation("Sending user details via SignalR.");
            await hubContext.Clients.User(userId).SendAsync("ReceiveUserDetails", userDetails);
        }
    }
}
