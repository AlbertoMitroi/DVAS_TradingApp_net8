using InternshipTradingApp.AccountManagement.Entities;

namespace InternshipTradingApp.AccountManagement.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
