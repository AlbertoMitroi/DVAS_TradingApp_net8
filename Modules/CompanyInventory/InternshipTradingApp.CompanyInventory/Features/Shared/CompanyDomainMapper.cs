using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Shared
{
    public static class CompanyDomainMapper
    {
        public static CompanyGetDTO ToCompanyGetDTO(this Company company)
        {
            return new CompanyGetDTO
            {
                Id = company.Id,
                Name = company.Name,
                Symbol = company.Symbol,
                Price = company.Price,
                OpeningPrice = company.OpeningPrice,
                ClosingPrice = company.ClosingPrice,
                ReferencePrice = company.ReferencePrice,
                DayVariation = company.DayVariation,
                EPS = company.EPS,
                PER = company.PER,
                Status = (int)company.Status
            };
        }

        public static IEnumerable<CompanyGetDTO> ToCompanyGetDTOs(this IEnumerable<Company> companies)
        {
            return companies.Select(company => company.ToCompanyGetDTO());
        }
    }
}
