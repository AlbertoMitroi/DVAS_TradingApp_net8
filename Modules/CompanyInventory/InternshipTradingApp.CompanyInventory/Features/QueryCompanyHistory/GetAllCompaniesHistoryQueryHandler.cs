using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory
{
    public class GetAllCompaniesHistoryQueryHandler(IQueryCompanyHistoryRepository queryCompanyHistoryRepository)
    {
        public async Task<IEnumerable<CompanyHistoryGetDTO>> Handle()
        {
            var allCompanies = await queryCompanyHistoryRepository.GetAllCompanies();
            return allCompanies.Select(companyHistory => companyHistory.ToCompanyHistoryGetDTO());
        }
    }
}
