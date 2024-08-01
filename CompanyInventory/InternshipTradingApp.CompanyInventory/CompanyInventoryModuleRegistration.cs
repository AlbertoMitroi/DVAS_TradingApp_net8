using InternshipTradingApp.CompanyInventory.Company;
using InternshipTradingApp.CompanyInventory.Company.Features.Add;
using InternshipTradingApp.CompanyInventory.Company.Features.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           // serviceCollection.AddScoped<GetAllCompaniesQueryHandler>();
        }
    }
}
