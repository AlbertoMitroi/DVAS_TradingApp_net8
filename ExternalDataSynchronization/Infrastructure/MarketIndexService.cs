using ExternalDataSynchronization.Domain.MarketIndex;
using Microsoft.EntityFrameworkCore;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;

namespace ExternalDataSynchronization.Infrastructure
{
    public class MarketIndexService : IMarketIndexService
    {
        private readonly IMarketIndexRepository _marketIndexRepository;  
        private readonly CompanyDbContext _dbContext;

        public MarketIndexService(IMarketIndexRepository marketIndexRepository, CompanyDbContext dbContext)
        {
            _marketIndexRepository = marketIndexRepository;
            _dbContext = dbContext;
        }

        public async Task<decimal> CalculateAndSaveMarketIndex()
        {
            var companies = await _dbContext.Companies
                .Include(c => c.CompanyHistoryEntries)
                .ToListAsync();

            var marketIndex = new MarketIndex();
            marketIndex.CalculateMarketIndex(companies);

            await _marketIndexRepository.SaveMarketIndexHistoryAsync(marketIndex.CurrentValue);

            return marketIndex.CurrentValue;
            Console.WriteLine(marketIndex.CurrentValue);
        }

        public async Task<IEnumerable<MarketIndexHistory>> GetMarketIndexHistory()
        {
            return await _marketIndexRepository.GetMarketIndexHistoriesAsync();
        }
    }
}
