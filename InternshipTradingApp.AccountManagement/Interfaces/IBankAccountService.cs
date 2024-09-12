using InternshipTradingApp.AccountManagement.DTOs;

namespace InternshipTradingApp.AccountManagement.Interfaces
{
    public interface IBankAccountService
    {
        Task<List<BankAccountResponseDto>> GetBankAccountsForUser(int userId);
        Task AddBankAccount(int userId, BankAccountDto bankAccountDto);
    }
}
