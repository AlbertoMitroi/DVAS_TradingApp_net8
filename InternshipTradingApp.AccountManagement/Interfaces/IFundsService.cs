
namespace InternshipTradingApp.AccountManagement.Interfaces
{
    public interface IFundsService
    {
        Task AddFundsAsync(int userId, decimal amount);
        Task WithdrawFundsAsync(int userId, int bankId, decimal amount);
    }

}
