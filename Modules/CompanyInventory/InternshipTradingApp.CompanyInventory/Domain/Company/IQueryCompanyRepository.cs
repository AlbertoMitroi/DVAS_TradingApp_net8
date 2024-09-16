namespace InternshipTradingApp.CompanyInventory.Domain
{
    public interface IQueryCompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies();
        Task<IEnumerable<Company>> GetCompaniesBySymbols(IEnumerable<string> symbols);
        Task<IEnumerable<Company>> GetAllCompaniesHistory(string? value);
        Task<Company?> GetCompanyBySymbol(string companySymbol);

        Task<IEnumerable<Company>> GetTopXCompanies(int? x, string? value, string orderToggle);

        Task<decimal> GetMarketIndex();


    }
}
