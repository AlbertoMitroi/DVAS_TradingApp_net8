namespace InternshipTradingApp.AccountManagement.Services
{
    public interface IUserNotificationService
    {
        Task SendUserDetailsAsync(string userId);
    }
}
