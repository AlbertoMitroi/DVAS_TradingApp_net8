using InternshipTradingApp.OrderManagementSystem.Entities;

namespace InternshipTradingApp.OrderManagementSystem.Helpers
{
    public static class OrderHelper
    {
        public static bool IsWithinPriceRange(Order buyOrder, Order sellOrder, decimal tolerance = 0.10m)
        {
            return buyOrder.Price >= sellOrder.Price * (1 - tolerance) &&
                   buyOrder.Price <= sellOrder.Price * (1 + tolerance);
        }
    }
}
