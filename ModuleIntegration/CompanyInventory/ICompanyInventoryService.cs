

namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public interface ICompanyInventoryService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompanies();
        Task<CompanyDTO?> GetCompanyBySymbol(string symbol);
        Task RegisterOrUpdateCompanies(IEnumerable<CompanyDTO> companies);

    }
}
