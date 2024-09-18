using InternshipTradingApp.CompanyInventory.Domain;
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
        public async Task<IEnumerable<Company>> GetAllCompaniesHistory(string? value)
        {
            var query = dbContext.Companies
                                 .Include(h => h.CompanyHistoryEntries)
                                 .AsQueryable();

            if (!string.IsNullOrEmpty(value))
            {
                query = query.Where(c => c.Symbol == value || c.Name == value);
            }

            var result = await query.ToListAsync();
            return result;
        }



        public async Task<Company?> GetCompanyBySymbol(string symbol)
        {
            return await dbContext.Companies
                                  .FirstOrDefaultAsync(c => c.Symbol == symbol);
        }


        public async Task<IEnumerable<Company>> GetTopXCompanies(int? x, string? value, string orderToggle)
        {
            x ??= 10;
            value = string.IsNullOrEmpty(value) ? "volume" : value.ToLower();

            bool isAscending = orderToggle == "asc";

            var query = dbContext.Companies
                .Include(c => c.CompanyHistoryEntries) 
                .Select(c => new
            {
                Company = c,
                LatestHistory = c.CompanyHistoryEntries
                    .OrderByDescending(che => che.Date)
                    .FirstOrDefault()
            });


            switch (value)
            {
                case "volume":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.Volume : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.Volume : 0);
                    break;
                case "price":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.Price : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.Price : 0);
                    break;
                case "eps":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.EPS : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.EPS : 0);
                    break;
                case "per":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.PER : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.PER : 0);
                    break;
                case "day variation":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.DayVariation : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.DayVariation : 0);
                    break;
                case "openingprice":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.OpeningPrice : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.OpeningPrice : 0);
                    break;
                case "closingprice":
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.ClosingPrice : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.ClosingPrice : 0);
                    break;
                case "symbol":
                    query = isAscending
                        ? query.OrderBy(c => c.Company.Symbol)
                        : query.OrderByDescending(c => c.Company.Symbol);
                    break;
                case "name":
                    query = isAscending
                        ? query.OrderBy(c => c.Company.Name)
                        : query.OrderByDescending(c => c.Company.Name);
                    break;
                default:
                    query = isAscending
                        ? query.OrderBy(c => c.LatestHistory != null ? c.LatestHistory.Volume : 0)
                        : query.OrderByDescending(c => c.LatestHistory != null ? c.LatestHistory.Volume : 0);
                    break;
            }

            
            var topCompaniesQuery = query.Take(x.Value);

            
            var sql = topCompaniesQuery.ToQueryString();

            
            var result = await topCompaniesQuery.Select(c => c.Company).ToListAsync();
            return result;
        }





        public async Task<decimal> GetMarketIndex()
        {
            var validPrices = await dbContext.Companies
                .Select(c => c.CompanyHistoryEntries
                    .OrderByDescending(che => che.Date)
                    .Select(che => (decimal?)che.Price) 
                    .FirstOrDefault())
                .Where(price => price != null) 
                .ToListAsync();

            if (!validPrices.Any())
            {
                return 0;
            }

            decimal marketIndex = validPrices.Average(price => price.Value);
            marketIndex = System.Math.Round(marketIndex, 4, MidpointRounding.AwayFromZero);
            return marketIndex;
        }
    }
}
