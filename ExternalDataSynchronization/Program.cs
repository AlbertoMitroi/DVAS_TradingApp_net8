using ExternalDataSynchronization.Domain.MarketIndex;
using ExternalDataSynchronization.Infrastructure.DataAccess;
using ExternalDataSynchronization.Infrastructure;
using InternshipTradingApp.CompanyInventory.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ExternalDataService.ConfigureServices();

        await ExternalDataService.ExecuteCommandsAsync(serviceProvider);


        var serviceCollection = new ServiceCollection();

        // Register the DbContext (MarketIndexDbContext) with Scoped lifetime
        serviceCollection.AddDbContext<MarketIndexDbContext>(options =>
            options.UseSqlServer("DefaultConnection")); // Replace with your actual connection string

        // Register the MarketIndexRepository and MarketIndexService with Scoped lifetime
        serviceCollection.AddScoped<IMarketIndexRepository, MarketIndexRepository>();
        serviceCollection.AddScoped<IMarketIndexService, MarketIndexService>();


    }
}