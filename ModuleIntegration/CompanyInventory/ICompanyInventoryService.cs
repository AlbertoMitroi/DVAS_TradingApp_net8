

namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public interface ICompanyInventoryService
    {
        Task<IEnumerable<CompanyGetDTO>> GetAllCompanies();
        Task<CompanyGetDTO?> GetCompanyBySymbol(string symbol);
        Task<IEnumerable<CompanyGetDTO>> RegisterOrUpdateCompanies(IEnumerable<CompanyAddDTO> companies);

    }
}
