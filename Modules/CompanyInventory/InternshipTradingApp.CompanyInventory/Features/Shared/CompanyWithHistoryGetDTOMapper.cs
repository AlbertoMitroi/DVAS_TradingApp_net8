using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.Shared
{
    public static class CompanyWithHistoryGetDTOMapper
    {
        public static CompanyWithHistoryGetDTO ToCompanyWithHistoryGetDTO(this Company company)
        {
            var companyDTO = company.ToCompanyGetDTO();
            var historyDTO = new List<CompanyHistoryGetDTO>();
            if (company.CompanyHistoryEntries != null)
            {
                historyDTO = company.CompanyHistoryEntries.ToCompanyHistoryGetDTOs().ToList();
            }
            return new CompanyWithHistoryGetDTO
            {
              Company = companyDTO,
              History=historyDTO
            };
        }
        public static IEnumerable<CompanyWithHistoryGetDTO> ToCompanyWithHistoryGetDTOs(this IEnumerable<Company> companies)
        {
            return companies.Select(company => company.ToCompanyWithHistoryGetDTO());
        }
    }
}
