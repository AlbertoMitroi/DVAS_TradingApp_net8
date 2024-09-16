using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.SignalR
{
    [Authorize]
    public class CompanyNotificationHub(ICompanyNotificationService notificationService, ILogger<CompanyNotificationHub> logger) : Hub
    {
        public async Task UpdateCompaniesData()
        {
            try
            {
                logger.LogInformation("UpdateCompaniesData method started");
                await notificationService.SendCompaniesDataAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in UpdateCompaniesData: {ex.Message}");
                throw;
            }
        }
    }
}
