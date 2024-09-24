using InternshipTradingApp.CompanyInventory.Domain.MarketIndex;
using InternshipTradingApp.CompanyInventory.Infrastructure.MarketIndexDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Infrastructure
{
    public class MarketIndexRepository : IMarketIndexRepository
    {
        private readonly MarketIndexDbContext _dbContext;

        public MarketIndexRepository(MarketIndexDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveMarketIndexHistoryAsync(decimal value)
        {
            var marketIndexHistory = new MarketIndexHistory
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Value = value
            };

            _dbContext.MarketIndexHistories.Add(marketIndexHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MarketIndexHistory>> GetMarketIndexHistoriesAsync()
        {
            return await _dbContext.MarketIndexHistories
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }

}
