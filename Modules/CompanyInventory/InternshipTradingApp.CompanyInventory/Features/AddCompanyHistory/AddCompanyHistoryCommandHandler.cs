using InternshipTradingApp.CompanyInventory.Domain;
using InternshipTradingApp.CompanyInventory.Domain.CompanyHistory;
using InternshipTradingApp.CompanyInventory.Features.Add;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.AddCompanyHistory
{
    public class AddCompanyHistoryCommandHandler(ICompanyHistoryEntryRepository repository, IQueryCompanyHistoryRepository queryCompanyHistoryRepository)
    {
        public async Task<List<CompanyHistoryEntry>> Handle(AddCompanyHistoryCommand command)
        {

            var addedEntries = new List<CompanyHistoryEntry>();

            foreach (var companyHistory in command.CompanyHistoryEntries)
            {
             
                    await repository.Add(companyHistory);
                    addedEntries.Add(companyHistory);
            }

            await repository.SaveChanges();

            return addedEntries;
        }
    }
}
