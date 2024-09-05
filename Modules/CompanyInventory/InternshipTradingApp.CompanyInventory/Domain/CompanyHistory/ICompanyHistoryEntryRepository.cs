using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Domain.CompanyHistory
{
    public interface ICompanyHistoryEntryRepository
    {
        Task<CompanyHistoryEntry> GetById(ulong id);
        Task Add(CompanyHistoryEntry historyEntry);
        Task SaveChanges();
    }
}
