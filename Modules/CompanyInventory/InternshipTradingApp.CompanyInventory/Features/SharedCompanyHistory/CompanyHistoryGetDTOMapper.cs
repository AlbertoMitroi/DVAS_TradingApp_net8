using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory
{
    public static class CompanyHistoryGetDTOMapper
    {
        public static CompanyHistoryEntry ToDomainObject(this CompanyHistoryGetDTO companyHistoryDto)
        {
            return CompanyHistoryEntry.Create(
                companyHistoryDto.CompanySymbol,
                companyHistoryDto.Price,
                companyHistoryDto.OpeningPrice,
                companyHistoryDto.ClosingPrice,
                companyHistoryDto.ReferencePrice,
                companyHistoryDto.EPS,
                companyHistoryDto.Volume
            );
        }

        public static IEnumerable<CompanyHistoryEntry> ToDomainObjects(this IEnumerable<CompanyHistoryGetDTO> companyHistoryDtos)
        {
            return companyHistoryDtos.Select(dto => dto.ToDomainObject());
        }
    }
}
