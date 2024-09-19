using InternshipTradingApp.OrderManagementSystem.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public class OrderMatchingBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<OrderMatchingBackgroundService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var orderMatchingEngine = scope.ServiceProvider.GetRequiredService<IOrderMatchingEngineService>();

                    try
                    {
                        logger.LogInformation("Starting to match pending orders.");
                        await orderMatchingEngine.MatchPendingOrdersAsync();
                        logger.LogInformation("Successfully matched pending orders.");

                        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error during background matching.");
                    }
                }
            }
        }
    }
}
