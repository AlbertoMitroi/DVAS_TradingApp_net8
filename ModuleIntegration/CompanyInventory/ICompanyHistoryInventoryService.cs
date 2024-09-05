

namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public interface ICompanyHistoryInventoryService
    {
        Task<IEnumerable<CompanyHistoryGetDTO>> GetAllCompanies();

        Task<IEnumerable<CompanyHistoryGetDTO>> RegisterCompaniesHistory(IEnumerable<CompanyHistoryAddDTO> companiesHistoryAddDtos);

        Task<CompanyWithHistoryGetDTO> GetCompanyWithHistoryDataAsync(string companySymbol);

    }
}
