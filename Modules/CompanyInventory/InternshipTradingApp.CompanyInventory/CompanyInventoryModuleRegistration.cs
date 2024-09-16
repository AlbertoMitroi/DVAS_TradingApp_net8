using InternshipTradingApp.CompanyHistoryInventory;
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Add;
using InternshipTradingApp.CompanyInventory.Features.AddCompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory;
using InternshipTradingApp.CompanyInventory.Infrastructure;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using InternshipTradingApp.CompanyInventory.SignalR;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using Microsoft.Extensions.DependencyInjection;


namespace InternshipTradingApp.CompanyInventory
{
    public static class CompanyInventoryModuleRegistration
    {
        public static void AddCompanyInventoryModule(this IServiceCollection serviceCollection)
        {
            RegisterRepositories(serviceCollection);
            RegisterCommandHandlers(serviceCollection);
            RegisterQueryHandlers(serviceCollection);
            serviceCollection.AddScoped<ICompanyInventoryService, CompanyInventoryService>();
            serviceCollection.AddScoped<ICompanyHistoryInventoryService, CompanyHistoryInventoryService>();
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            // serviceCollection.AddHostedService<PriceGenerationService>(); // PRICE GENERATION
            serviceCollection.AddScoped<ICompanyNotificationService, CompanyNotificationService>();
            serviceCollection.AddScoped<ICompanyRepository, CompanyRepository>();
            serviceCollection.AddScoped<IQueryCompanyRepository, QueryCompanyRepository>();
            serviceCollection.AddScoped<ICompanyHistoryEntryRepository, CompanyHistoryRepository>();
            serviceCollection.AddScoped<IQueryCompanyHistoryRepository, QueryCompanyHistoryRepository>();
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AddOrUpdateCompaniesCommandHandler>();
            serviceCollection.AddScoped<AddCompanyHistoryCommandHandler>();

        }

        private static void RegisterQueryHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<GetCompanyHistoryBySymbolQueryHandler>();
            serviceCollection.AddScoped<Features.Query.GetAllCompaniesHistoryQueryHandler>();
            serviceCollection.AddScoped<Features.QueryCompanyHistory.GetAllCompaniesHistoryQueryHandler>();
            serviceCollection.AddScoped<GetCompanyWithHistoryDataQueryHandler>();
            serviceCollection.AddScoped<GetTopXCompaniesQueryHandler>();
        }
    }
}
