/*
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternshipTradingApp.CompanyInventory.Infrastructure
{
    public class PriceGenerationService : BackgroundService
    {
        private readonly ILogger<PriceGenerationService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly Random _random = new Random();

        public PriceGenerationService(ILogger<PriceGenerationService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Price Generation Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var companyInventoryService = scope.ServiceProvider.GetRequiredService<ICompanyInventoryService>();
                    await UpdatePricesAsync(companyInventoryService);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Price Generation Service is stopping.");
        }

        private async Task UpdatePricesAsync(ICompanyInventoryService companyInventoryService)
        {
            var companyDtos = await companyInventoryService.GetAllCompanies();
            var companies = companyDtos.ToDomainObjects();
            var updatedCompanyDtos = new List<CompanyAddDTO>();

            foreach (var company in companies)
            {
                var variationPercentage = (decimal)(_random.NextDouble() * 0.1 - 0.05);
                var newPrice = company.Price * (1 + variationPercentage);

                try
                {
                    company.UpdatePrice(newPrice);
                    if (company == null) throw new ArgumentNullException(nameof(company));
                    updatedCompanyDtos.Add(company.ToCompanyAddDTO());
                    _logger.LogInformation($"Updated {company.Name} ({company.Symbol}) price to {newPrice:C}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to update price for {company.Name} ({company.Symbol})");
                }
            }

           // await companyInventoryService.RegisterOrUpdateCompanies(updatedCompanyDtos);
        }
    }
}
*/