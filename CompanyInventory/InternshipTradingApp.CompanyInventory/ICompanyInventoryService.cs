using InternshipTradingApp.CompanyInventory.Company.Features.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory
{
    public interface ICompanyInventoryService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompanies();
        Task<CompanyDTO?> GetCompanyBySymbol(string symbol);
        Task RegisterOrUpdateCompanies(IEnumerable<CompanyDTO> companies);  

    }
}
