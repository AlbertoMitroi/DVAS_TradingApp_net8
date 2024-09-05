using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess
{
    public class CompanyHistoryRepository(CompanyDbContext dbContext) : ICompanyHistoryEntryRepository
    {

        public async Task<CompanyHistoryEntry> GetById(ulong id)
        {
            var companyHistory = await dbContext.CompanyHistoryEntries.FindAsync(id);
            if (companyHistory == null)
            {
                throw new Exception($"Company history with ID {id} not found.");
            }
            return companyHistory;
        }

        public async Task Add(CompanyHistoryEntry entry)
        {
            if (string.IsNullOrEmpty(entry.CompanySymbol))
            {
                throw new ArgumentException("CompanySymbol cannot be null or empty");
            }

            dbContext.CompanyHistoryEntries.Add(entry);
            await dbContext.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
