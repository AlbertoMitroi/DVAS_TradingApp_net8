using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Shared
{
    public static class CompanyAddDTOMapper
    {
        public static Company ToDomainObject(this CompanyAddDTO companyAddDto)
        {
            return Company.Create(
                companyAddDto.Name,
                companyAddDto.Symbol,
                companyAddDto.Price,
                companyAddDto.OpeningPrice,
                companyAddDto.ClosingPrice,
                companyAddDto.ReferencePrice,
                companyAddDto.EPS
            );
        }

        public static IEnumerable<Company> ToDomainObjects(this IEnumerable<CompanyAddDTO> companyAddDtos)
        {
            return companyAddDtos.Select(dto => dto.ToDomainObject());
        }

        public static CompanyGetDTO ToCompanyGetDTO(this CompanyAddDTO companyAddDto)
        {
            return new CompanyGetDTO
            {
                Name = companyAddDto.Name,
                Symbol = companyAddDto.Symbol,
                Price = companyAddDto.Price,
                OpeningPrice = companyAddDto.OpeningPrice,
                ClosingPrice = companyAddDto.ClosingPrice,
                ReferencePrice = companyAddDto.ReferencePrice,
                DayVariation = companyAddDto.DayVariation,
                EPS = companyAddDto.EPS,
                PER = companyAddDto.PER,
                Status = (int)companyAddDto.Status
            };
        }

        public static IEnumerable<CompanyGetDTO> ToCompanyGetDTOs(this IEnumerable<CompanyAddDTO> companyAddDtos)
        {
            return companyAddDtos.Select(dto => dto.ToCompanyGetDTO());
        }
    }
}
