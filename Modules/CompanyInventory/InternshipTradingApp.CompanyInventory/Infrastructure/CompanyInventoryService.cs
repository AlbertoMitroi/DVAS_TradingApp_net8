using InternshipTradingApp.CompanyInventory.Features.Add;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory
{
    internal class CompanyInventoryService(
        GetAllCompaniesHistoryQueryHandler getAllCompaniesQueryHandler,
        GetCompanyHistoryBySymbolQueryHandler getCompanyBySymbolQueryHandler,
        AddOrUpdateCompaniesCommandHandler addOrUpdateCompaniesCommandHandler,
        GetTopXCompaniesQueryHandler getTopXCompaniesQueryHandler
       ) : ICompanyInventoryService
    
    
    {
        public async Task<IEnumerable<CompanyWithHistoryGetDTO>> GetAllCompanies()
        {
            return await getAllCompaniesQueryHandler.Handle();
        }

        public async Task<CompanyGetDTO?> GetCompanyBySymbol(string symbol)
        {
            var query = new GetCompanyHistoryBySymbolQuery { Symbol = symbol };
            return await getCompanyBySymbolQueryHandler.Handle(query);
        }

        public async Task<IEnumerable<CompanyGetDTO>> RegisterOrUpdateCompanies(IEnumerable<CompanyAddDTO> companyAddDtos)
        {
            var companyDomain = companyAddDtos.ToDomainObjects();
            var addOrUpdateCompanies = new AddOrUpdateCompaniesCommand { companies = companyDomain.ToList() };
            var company = await addOrUpdateCompaniesCommandHandler.Handle(addOrUpdateCompanies);

            return company.ToCompanyGetDTOs();
        }

        public async Task<IEnumerable<CompanyWithHistoryGetDTO>> GetTopXCompanies(int? x, string? value) {
            var query = new GetTopXCompaniesQuery { X = x, Value = value };
            return await getTopXCompaniesQueryHandler.Handle(query);
        }
    }
}