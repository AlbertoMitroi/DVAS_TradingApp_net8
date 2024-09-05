using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.AddCompanyHistory
{
    public class AddCompanyHistoryCommand
    {
       
        public List<CompanyHistoryEntry> CompanyHistoryEntries = new List<CompanyHistoryEntry>();

    }
}
