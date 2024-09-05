using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Shared
{
    public static class CompanyHistoryAddDTOMapper
    {
        public static CompanyHistoryEntry ToDomainObject(this CompanyHistoryAddDTO companyHistoryAddDto)
        {
            return CompanyHistoryEntry.Create(
                companyHistoryAddDto.CompanySymbol,
                companyHistoryAddDto.Price,
                companyHistoryAddDto.OpeningPrice,
                companyHistoryAddDto.ClosingPrice,
                companyHistoryAddDto.ReferencePrice,
                companyHistoryAddDto.EPS
            );
        }

        public static IEnumerable<CompanyHistoryEntry> ToDomainObjects(this IEnumerable<CompanyHistoryAddDTO> companyHistoryAddDtos)
        {
            return companyHistoryAddDtos.Select(dto => dto.ToDomainObject());
        }

        public static CompanyHistoryGetDTO ToCompanyHistoryGetDTO(this CompanyHistoryAddDTO companyHistoryAddDto)
        {
            return new CompanyHistoryGetDTO
            {
                CompanySymbol = companyHistoryAddDto.CompanySymbol.ToString(),
                Price = companyHistoryAddDto.Price,
                OpeningPrice = companyHistoryAddDto.OpeningPrice,
                ClosingPrice = companyHistoryAddDto.ClosingPrice,
                ReferencePrice = companyHistoryAddDto.ReferencePrice,
                DayVariation = companyHistoryAddDto.DayVariation,
                EPS = companyHistoryAddDto.EPS,
                PER = companyHistoryAddDto.PER,
                Date = companyHistoryAddDto.Date
            };
        }

        public static IEnumerable<CompanyHistoryGetDTO> ToCompanyHistoryGetDTOs(this IEnumerable<CompanyHistoryAddDTO> companyHistoryAddDtos)
        {
            return companyHistoryAddDtos.Select(dto => dto.ToCompanyHistoryGetDTO());
        }
    }
}
