using InternshipTradingApp.CompanyInventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Domain.MarketIndex
{
    public class MarketIndex
    {
        public decimal CurrentValue { get; private set; }

        public void CalculateMarketIndex(IEnumerable<Company> companies)
        {
            var validPrices = companies
                .Select(c => c.CompanyHistoryEntries
                    .OrderByDescending(che => che.Date)
                    .Select(che => (decimal?)che.Price)
                    .FirstOrDefault())
                .Where(price => price != null)
                .ToList();

            if (validPrices != null)
            {
                CurrentValue = validPrices.Average(price => price.Value);
                CurrentValue = System.Math.Round(CurrentValue, 4, MidpointRounding.AwayFromZero);
            }
        }
    }
}
