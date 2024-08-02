
namespace InternshipTradingApp.CompanyInventory.Domain
{
    internal interface IQueryCompanyRepository
    {
        Task<IQueryable<Company>> GetAll();
        Task<Company?> GetCompanyBySymbol(string symbol);
    }
}
