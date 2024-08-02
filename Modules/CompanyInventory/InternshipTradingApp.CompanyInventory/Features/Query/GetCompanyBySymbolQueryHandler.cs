using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;


namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    internal class GetCompanyBySymbolQueryHandler
    {
        private readonly IQueryCompanyRepository companyQueryRepository;
        public GetCompanyBySymbolQueryHandler(IQueryCompanyRepository companyRepository)
        {
            companyQueryRepository = companyRepository;
        }
        //return CompanyDTO for now. this will be changed later to GetCompanyQueryResult
        public async Task<CompanyDTO?> Handle(GetCompanyBySymbolQuery query)
        {
            var companySymbol = query.Symbol;
            var company = await companyQueryRepository.GetCompanyBySymbol(companySymbol);
            if (company != default)
                return company.ToDTO();

            return default;
        }
    }
}
