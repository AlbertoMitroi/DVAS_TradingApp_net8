using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory
{
    public static class CompanyAddDTOMapper
    {
        public static Company ToDomainObject(this CompanyAddDTO companyAddDto)
        {
            return Company.Create(
                companyAddDto.Name,
                companyAddDto.Symbol
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
                Status = companyAddDto.Status
            };
        }

        public static IEnumerable<CompanyGetDTO> ToCompanyGetDTOs(this IEnumerable<CompanyAddDTO> companyAddDtos)
        {
            return companyAddDtos.Select(dto => dto.ToCompanyGetDTO());
        }
    }
}
