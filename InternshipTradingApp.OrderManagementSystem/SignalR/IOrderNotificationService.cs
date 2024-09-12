namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public interface IOrderNotificationService
    {
        Task SendOrderDetailsAsync(string userId);
    }
}
