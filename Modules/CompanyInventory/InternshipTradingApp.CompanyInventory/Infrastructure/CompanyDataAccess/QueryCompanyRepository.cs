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
        public async Task<IEnumerable<Company>> GetAllCompaniesHistory(string symbol)
        {
            var result = await dbContext.Companies
                                        .Include(h => h.CompanyHistoryEntries)
                                        .Where(c => c.Symbol == symbol)
                                        .ToListAsync();
            return result;
        }


        public async Task<Company?> GetCompanyBySymbol(string symbol)
        {
            return await dbContext.Companies
                                  .FirstOrDefaultAsync(c => c.Symbol == symbol);
        }


        public async Task<IEnumerable<Company>> GetTopXCompanies(int? x, string? value)
        {

            x ??= 10;

            var query = dbContext.Companies
                .Include(c => c.CompanyHistoryEntries)
                .Select(c => new
                {
                    Company = c,
                    LatestHistory = c.CompanyHistoryEntries.OrderByDescending(che => che.Date).FirstOrDefault()  // Get the latest entry by date
                });

            switch (value?.ToLower())
            {
                case "volume":
                    query = query.OrderByDescending(c => c.LatestHistory.Volume);
                    break;
                case "price":
                    query = query.OrderByDescending(c => c.LatestHistory.Price);
                    break;
                case "eps":
                    query = query.OrderByDescending(c => c.LatestHistory.EPS);
                    break;
                case "per":
                    query = query.OrderByDescending(c => c.LatestHistory.PER);
                    break;
                case "dayvariation":
                    query = query.OrderByDescending(c => c.LatestHistory.DayVariation);
                    break;
                case "openingprice":
                    query = query.OrderByDescending(c => c.LatestHistory.OpeningPrice);
                    break;
                case "closingprice":
                    query = query.OrderByDescending(c => c.LatestHistory.ClosingPrice);
                    break;
                default:
                    query = query.OrderByDescending(c => c.LatestHistory.Volume);
                    break;
            }


            var topCompaniesQuery = query.Take(x.Value);


            var sql = topCompaniesQuery.ToQueryString();

            var result = await topCompaniesQuery.Select(c => c.Company).ToListAsync();
            return result;
        }

    }
}
