using ExternalDataSynchronization.Infrastructure;
using InternshipTradingApp.CompanyInventory.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ExternalDataSynchronization.Infrastructure;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ExternalDataService.ConfigureServices();

        await ExternalDataService.ExecuteCommandsAsync(serviceProvider);


        var serviceCollection = new ServiceCollection();
    }
}