using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory
{
    public static class CompanyHistoryDomainMapper
    {
        public static CompanyHistoryGetDTO ToCompanyHistoryGetDTO(this CompanyHistoryEntry company)
        {
            return new CompanyHistoryGetDTO
            {
                Id = company.Id,
                CompanySymbol = company.CompanySymbol,
                Price = company.Price,
                OpeningPrice = company.OpeningPrice,
                ClosingPrice = company.ClosingPrice,
                ReferencePrice = company.ReferencePrice,
                DayVariation = company.DayVariation,
                EPS = company.EPS,
                PER = company.PER,
                Date=company.Date

            };
        }

        public static IEnumerable<CompanyHistoryGetDTO> ToCompanyHistoryGetDTOs(this IEnumerable<CompanyHistoryEntry> companies)
        {
            return companies.Select(company => company.ToCompanyHistoryGetDTO());
        }
    }
}
