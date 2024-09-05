using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    internal class QueryCompanyHistoryRepository (CompanyDbContext dbContext) : IQueryCompanyHistoryRepository
    {

        public async Task<IQueryable<CompanyHistoryEntry>> GetAllCompanies()
        {
            var companies = dbContext.CompanyHistoryEntries.AsQueryable();
            return await Task.FromResult(companies);
        }

        public async Task<IEnumerable<CompanyHistoryEntry>> GetCompaniesBySymbols(IEnumerable<string> symbols)
        {
            return await dbContext.CompanyHistoryEntries
                .Where(entry => symbols.Contains(entry.CompanySymbol))
                .ToListAsync();
        }

    }
}
