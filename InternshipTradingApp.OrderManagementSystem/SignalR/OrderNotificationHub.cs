using InternshipTradingApp.OrderManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.SignalR
{
    [Authorize]
    public class OrderNotificationHub(IOrderNotificationService orderNotificationService, ILogger<OrderNotificationHub> logger) : Hub
    {
        public async Task SendOrderUpdate()
        {
            logger.LogInformation("SendOrder method started");

            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                logger.LogError("User ID not found in the claims.");
                throw new HubException("User ID not found.");
            }

            logger.LogInformation($"Notifying user with ID: {userId}");
            await orderNotificationService.SendOrderDetailsAsync(userId);
        }
    }
}


