using Microsoft.AspNetCore.SignalR;
using InternshipTradingApp.AccountManagement.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace InternshipTradingApp.Server.SignalR
{
    [Authorize]
    public class UserNotificationHub(IUserNotificationService notificationService, ILogger<UserNotificationHub> logger) : Hub
    {
        public async Task SendData()
        {
            logger.LogInformation("SendData method started");

            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                logger.LogError("User ID not found in the claims.");
                throw new HubException("User ID not found.");
            }

            logger.LogInformation($"Notifying user with ID: {userId}");
            await notificationService.SendUserDetailsAsync(userId);
        }
    }
}
