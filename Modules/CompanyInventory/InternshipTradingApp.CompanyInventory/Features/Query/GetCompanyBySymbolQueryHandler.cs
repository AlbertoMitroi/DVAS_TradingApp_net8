using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    internal class GetCompanyBySymbolQueryHandler(IQueryCompanyRepository queryCompanyRepository)
    {
        public async Task<CompanyGetDTO?> Handle(GetCompanyBySymbolQuery query)
        {
            if (string.IsNullOrEmpty(query.Symbol))
            {
                throw new ArgumentException("Symbol must be provided.", nameof(query.Symbol));
            }

            var company = await queryCompanyRepository.GetCompanyBySymbol(query.Symbol);
            return company?.ToCompanyGetDTO();
        }
    }
}
