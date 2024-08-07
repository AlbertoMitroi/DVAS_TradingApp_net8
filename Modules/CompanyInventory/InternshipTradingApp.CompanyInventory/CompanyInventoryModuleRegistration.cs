using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Add;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
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
        }

        private static void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICompanyRepository, CompanyRepository>();
            serviceCollection.AddScoped<IQueryCompanyRepository, QueryCompanyRepository>();
        }

        private static void RegisterCommandHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AddOrUpdateCompaniesCommandHandler>();            
        }

        private static void RegisterQueryHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<GetCompanyBySymbolQueryHandler>();
        }
    }
}
