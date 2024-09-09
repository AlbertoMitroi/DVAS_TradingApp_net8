

namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public interface ICompanyInventoryService
    {
        Task<IEnumerable<CompanyWithHistoryGetDTO>> GetAllCompanies();
        Task<CompanyGetDTO?> GetCompanyBySymbol(string symbol);
        Task<IEnumerable<CompanyGetDTO>> RegisterOrUpdateCompanies(IEnumerable<CompanyAddDTO> companies);

        Task<IEnumerable<CompanyWithHistoryGetDTO>> GetTopXCompanies(int? x, string? value);

    }
}
