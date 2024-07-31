using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Company.Features.Shared
{
    internal static class CompanyMapper
    {
        public static Company ToDomainObject(this CompanyDTO company)
        {
            return Company.Create(
                    company.Name, 
                    company.Symbol, 
                    company.Price, 
                    company.OpeningPrice, 
                    company.ClosingPrice, 
                    company.ReferencePrice, 
                    company.EPS
                );
        }
    }
}
