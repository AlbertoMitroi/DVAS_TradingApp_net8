
using InternshipTradingApp.CompanyInventory.Domain;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    internal class QueryCompanyRepository : IQueryCompanyRepository
    {
        public async Task<IQueryable<Company>> GetAll()
        {
            return new List<Company>()
                   .AsQueryable();
        }

        public async Task<Company?> GetCompanyBySymbol(string symbol)
        {
            return Company.Create("asdasd", "asdasd", 2, 3, 4, 5, 6);//default(Company);
        }
    }
}
