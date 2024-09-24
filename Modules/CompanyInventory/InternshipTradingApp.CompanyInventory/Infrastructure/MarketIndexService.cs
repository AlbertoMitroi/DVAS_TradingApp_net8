using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.MarketIndex;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Infrastructure
{
    public class MarketIndexService : IMarketIndexService
    {
        private readonly IMarketIndexRepository _marketIndexRepository;
        private readonly IQueryCompanyRepository _queryCompanyRepository;
        

        public MarketIndexService(IMarketIndexRepository marketIndexRepository, IQueryCompanyRepository queryCompanyRepository)
        {
            _marketIndexRepository = marketIndexRepository;
            _queryCompanyRepository = queryCompanyRepository;
        }

        public async Task<decimal> CalculateAndSaveMarketIndex()
        {
            var companies = await _queryCompanyRepository.GetAllCompanies();

            var marketIndex = new MarketIndex();
            marketIndex.CalculateMarketIndex(companies);

            var marketIndexHistories = await _marketIndexRepository.GetMarketIndexHistoriesAsync();
            var lastSavedMarketIndex = marketIndexHistories.FirstOrDefault();

            if (lastSavedMarketIndex == null || lastSavedMarketIndex.Date != DateOnly.FromDateTime(DateTime.Now))
            {
                await _marketIndexRepository.SaveMarketIndexHistoryAsync(marketIndex.CurrentValue);
            }

            return marketIndex.CurrentValue;
            Console.WriteLine(marketIndex.CurrentValue);
        }

        public async Task<IEnumerable<MarketIndexHistory>> GetMarketIndexHistory()
        {
            return await _marketIndexRepository.GetMarketIndexHistoriesAsync();
        }
    }
}
