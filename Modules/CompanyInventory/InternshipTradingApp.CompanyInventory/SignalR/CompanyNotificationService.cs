using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.SignalR
{
    public class CompanyNotificationService(
        ICompanyInventoryService companyInventoryService,
        IHubContext<CompanyNotificationHub> hubContext) : ICompanyNotificationService
    {
        public async Task SendCompaniesDataAsync()
        {
            var allCompanies = await companyInventoryService.GetAllCompanies();

            await hubContext.Clients.All.SendAsync("ReceiveCompanies", allCompanies);
        }
    }
}
