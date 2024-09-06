using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    public class GetAllCompaniesHistoryQueryHandler(IQueryCompanyRepository queryCompanyRepository)
    {
        public async Task<IEnumerable<CompanyWithHistoryGetDTO>> Handle()
        {
            var allCompanies = await queryCompanyRepository.GetAllCompanies();
            return allCompanies.Select(company => company.ToCompanyWithHistoryGetDTO());
        }
    }
}
