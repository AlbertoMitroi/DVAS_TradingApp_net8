using InternshipTradingApp.CompanyInventory.Infrastructure;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceProvider = ExternalDataService.ConfigureServices();

        await ExternalDataService.ExecuteCommandsAsync(serviceProvider);

    }
}