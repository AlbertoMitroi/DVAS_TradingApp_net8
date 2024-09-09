using InternshipTradingApp.CompanyInventory.Features.AddCompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Query;
using InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.CompanyInventory.Features.SharedCompanyHistory;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyHistoryInventory
{
    internal class CompanyHistoryInventoryService(
    InternshipTradingApp.CompanyInventory.Features.QueryCompanyHistory.GetAllCompaniesHistoryQueryHandler getAllCompaniesHistoryQueryHandler,
    AddCompanyHistoryCommandHandler addCompanyHistoryCommandHandler,
    GetCompanyWithHistoryDataQueryHandler getCompanyWithHistoryDataQueryHandler
    ) : ICompanyHistoryInventoryService
    {
      
        public async Task<IEnumerable<CompanyHistoryGetDTO>> GetAllCompanies()
        {
            return await getAllCompaniesHistoryQueryHandler.Handle();
        }

        public async Task<CompanyWithHistoryGetDTO> GetCompanyWithHistoryDataAsync(string companySymbol)
        {
            var query = new GetCompanyWithHistoryDataQuery
            {
                CompanySymbol = companySymbol
            };

            var result = await getCompanyWithHistoryDataQueryHandler.Handle(query);

            if (result == null)
            {
                throw new Exception($"Company with symbol {companySymbol} not found.");
            }

            return result;
        }

        public async Task<IEnumerable<CompanyHistoryGetDTO>> RegisterCompaniesHistory(IEnumerable<CompanyHistoryAddDTO> companiesHistoryAddDtos)
        {
            var companyHistoryDomain = companiesHistoryAddDtos.ToDomainObjects();
            var addCompaniesHistory = new AddCompanyHistoryCommand
            {
                CompanyHistoryEntries = companyHistoryDomain.ToList()
            };
            var companyHistory = await addCompanyHistoryCommandHandler.Handle(addCompaniesHistory);

            return companyHistory.ToCompanyHistoryGetDTOs();
        }

    }
}
