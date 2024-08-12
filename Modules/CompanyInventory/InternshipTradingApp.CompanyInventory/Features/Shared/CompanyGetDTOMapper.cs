using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;

namespace InternshipTradingApp.CompanyInventory.Features.Shared
{
    public static class CompanyGetDTOMapper
    {
        public static Company ToDomainObject(this CompanyGetDTO companyDto)
        {
            return Company.Create(
                companyDto.Name,
                companyDto.Symbol,
                companyDto.Price,
                companyDto.OpeningPrice,
                companyDto.ClosingPrice,
                companyDto.ReferencePrice,
                companyDto.EPS
            );
        }

        public static IEnumerable<Company> ToDomainObjects(this IEnumerable<CompanyGetDTO> companyDtos)
        {
            return companyDtos.Select(dto => dto.ToDomainObject());
        }
    }
}
