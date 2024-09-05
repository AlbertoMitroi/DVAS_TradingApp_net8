using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Domain.CompanyHistory
{
    public interface IQueryCompanyHistoryRepository
    {
        Task<IQueryable<CompanyHistoryEntry>> GetAllCompanies();
        Task<IEnumerable<CompanyHistoryEntry>> GetCompaniesBySymbols(IEnumerable<string> companySymbol);
    }
}
