
using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Features.Shared;
using InternshipTradingApp.ModuleIntegration.CompanyInventory;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    internal class QueryCompanyRepository(CompanyDbContext dbContext) : IQueryCompanyRepository
    {
        public async Task<IEnumerable<Company>> GetAllCompanies()
        {
            var companies = await dbContext.Companies.Include(h => h.CompanyHistoryEntries)
                                               .ToListAsync();
            return companies;
        }


        public async Task<IEnumerable<Company>> GetCompaniesBySymbols(IEnumerable<string> symbols)
        {
            return await dbContext.Companies
                                 .Where(c => symbols.Contains(c.Symbol))
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Company>> GetAllCompaniesHistory(string symbol)
        {
            var result = await dbContext.Companies
                                        .Include(h=>h.CompanyHistoryEntries)
                                        .Where(c=>c.Symbol==symbol)
                                        .ToListAsync();
            return result;
        }


        public async Task<Company?> GetCompanyBySymbol(string symbol)
        {
            return await dbContext.Companies
                                  .FirstOrDefaultAsync(c => c.Symbol == symbol);
        }
    }
}
