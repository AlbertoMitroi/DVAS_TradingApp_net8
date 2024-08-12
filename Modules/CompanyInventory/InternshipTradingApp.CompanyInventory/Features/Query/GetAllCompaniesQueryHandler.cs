using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    public class GetAllCompaniesQueryHandler(IQueryCompanyRepository queryCompanyRepository)
    {
        public async Task<IEnumerable<CompanyGetDTO>> Handle()
        {
            var allCompanies = await queryCompanyRepository.GetAllCompanies();
            return allCompanies.Select(company => company.ToCompanyGetDTO());
        }
    }
}
