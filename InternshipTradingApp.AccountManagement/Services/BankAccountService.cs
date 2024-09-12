using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using InternshipTradingApp.Server.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.AccountManagement.Services
{
    public class BankAccountService(AccountDbContext context, IUserNotificationService userNotification) : IBankAccountService
    {
        public async Task<List<BankAccountResponseDto>> GetBankAccountsForUser(int userId)
        {
            return await context.BankAccounts
                .Where(b => b.UserId == userId)
                .Select(b => new BankAccountResponseDto
                {
                    Id = b.Id,
                    IBAN = b.IBAN,
                    BankName = b.BankName
                })
                .ToListAsync();
        }

        public async Task AddBankAccount(int userId, BankAccountDto bankAccountDto)
        {
            var user = await context.Users.FindAsync(userId) ?? throw new Exception("User not found");
            if (bankAccountDto.BankName.Length < 10) throw new Exception("Bank name must be greater than 10 characters");
            if (bankAccountDto.IBAN.Length < 12) throw new Exception("IBAN must be greater than 10 caracters");

            var bankAccount = new BankAccount
            {
                UserId = userId,
                User = user,
                IBAN = bankAccountDto.IBAN,
                BankName = bankAccountDto.BankName
            };

            context.BankAccounts.Add(bankAccount);
            await context.SaveChangesAsync();

            if (userId.ToString() == null) throw new Exception("UserId is null");
            await userNotification.SendUserDetailsAsync(userId.ToString());

            var addedBankAccount = await context.BankAccounts
                .FirstOrDefaultAsync(b => b.UserId == userId && b.IBAN == bankAccountDto.IBAN) 
                ?? throw new Exception("Failed to add bank account.");
        }
    }
}
