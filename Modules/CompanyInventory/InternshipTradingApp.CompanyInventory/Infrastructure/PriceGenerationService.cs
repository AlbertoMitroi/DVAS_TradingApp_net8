using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.SignalR;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

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
                    var companyHistoryInventoryService = scope.ServiceProvider.GetRequiredService<ICompanyHistoryInventoryService>();
                    var companyNotificationService = scope.ServiceProvider.GetRequiredService<ICompanyNotificationService>();

                    await UpdatePricesAsync(companyInventoryService, companyHistoryInventoryService, companyNotificationService);
                }

                // 10 seconds delay
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("Price Generation Service is stopping.");
        }

        private async Task UpdatePricesAsync(ICompanyInventoryService companyInventoryService, ICompanyHistoryInventoryService companyHistoryInventoryService, ICompanyNotificationService companyNotificationService)
        {
            var companyGetDto = await companyInventoryService.GetAllCompanies();

            var updatedCompanyDtos = new List<CompanyHistoryAddDTO>();

            foreach (var company in companyGetDto)
            {
                var lastHistory = company.History.LastOrDefault();

                if (lastHistory == null)
                {
                    _logger.LogWarning($"No history found for company {company.Company.Name} ({company.Company.Symbol})");
                    continue;
                }

                var referencePrice = lastHistory.ClosingPrice;

                var variationPercentage = (decimal)(_random.NextDouble() * 0.04 - 0.02); // -2% <=> +2%
                var newPrice = Math.Round(referencePrice * (1 + variationPercentage), 3);

                var openingPrice = lastHistory.OpeningPrice;
                var closingPrice = newPrice;
                var dayVariation = (newPrice - openingPrice) / openingPrice * 100;
                var volume = _random.Next(100, 10000);

                try
                {
                    var addNewCompanyPrice = new CompanyHistoryAddDTO
                    {
                        CompanySymbol = company.Company.Symbol,
                        Price = newPrice,
                        ReferencePrice = referencePrice,
                        OpeningPrice = openingPrice,
                        ClosingPrice = closingPrice,
                        PER = lastHistory.PER,
                        DayVariation = dayVariation,
                        EPS = lastHistory.EPS,
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Volume = volume
                    };
                    updatedCompanyDtos.Add(addNewCompanyPrice);

                    _logger.LogInformation($"Updated {company.Company.Name} ({company.Company.Symbol}) price to {newPrice:C}, day variation: {dayVariation:F2}%, volume: {volume}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to update price for {company.Company.Name} ({company.Company.Symbol})");
                }
            }

            await companyHistoryInventoryService.RegisterCompaniesHistory(updatedCompanyDtos);
            await companyNotificationService.SendCompaniesDataAsync();
        }
    }

}
