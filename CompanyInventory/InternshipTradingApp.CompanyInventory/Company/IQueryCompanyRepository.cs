using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Company
{
    internal interface IQueryCompanyRepository
    {
        Task<IQueryable<Company>> GetAll();
        Task<IQueryable<Company>> GetCompanyBySymbol(string symbol);
    }
}
