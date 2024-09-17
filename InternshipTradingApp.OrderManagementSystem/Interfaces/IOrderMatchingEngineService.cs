

using InternshipTradingApp.OrderManagementSystem.Entities;

namespace InternshipTradingApp.OrderManagementSystem.Interfaces
{
    public interface IOrderMatchingEngineService
    {
        Task MatchOrderAsync(Order newOrder);
        Task MatchPendingOrdersAsync();
    }
}
