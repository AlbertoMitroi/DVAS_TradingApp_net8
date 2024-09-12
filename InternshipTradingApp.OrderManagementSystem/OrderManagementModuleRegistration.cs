using InternshipTradingApp.OrderManagementSystem.Data;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using InternshipTradingApp.OrderManagementSystem.Services;
using InternshipTradingApp.OrderManagementSystem.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace InternshipTradingApp.OrderManagementSystem
{
    public static class OrderManagementModuleRegistration
    {
        public static void AddOrderManagementModule(this IServiceCollection serviceCollection)
        {
            RegisterRepositories(serviceCollection);
            RegisterServices(serviceCollection);
            RegisterSignalR(serviceCollection);
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        }

        private static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IOrderService, OrderService>();
           // serviceCollection.AddScoped<IOrderNotificationService, OrderNotificationService>();
        }

        private static void RegisterSignalR(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<OrderNotificationHub>();
        }
    }
}
