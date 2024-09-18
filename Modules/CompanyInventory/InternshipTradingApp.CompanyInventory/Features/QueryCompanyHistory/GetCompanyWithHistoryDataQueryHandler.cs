using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory
{
    
    public class GetCompanyWithHistoryDataQueryHandler(IQueryCompanyRepository queryCompanyRepository)
    {
        public async Task<CompanyWithHistoryGetDTO> Handle(GetCompanyWithHistoryDataQuery? getCompanyWithHistoryDataQuery)
        {
            var value = getCompanyWithHistoryDataQuery?.Value ?? "symbol";
            var companies = await queryCompanyRepository.GetAllCompaniesHistory(getCompanyWithHistoryDataQuery.Value);
            var company = companies.FirstOrDefault();
            if (company == null)
            {
                throw new Exception($"Company with this value {getCompanyWithHistoryDataQuery.Value} not found.");
            }
            var result = company.ToCompanyWithHistoryGetDTO();

            return result;
        }
    }
}