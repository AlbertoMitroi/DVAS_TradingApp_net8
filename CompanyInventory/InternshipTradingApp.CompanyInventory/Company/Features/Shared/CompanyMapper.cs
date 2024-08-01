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

        public static CompanyDTO ToDTO(this Company company) 
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                Symbol = company.Symbol,
                Price = company.Price,
                OpeningPrice = company.OpeningPrice,
                ClosingPrice = company.ClosingPrice,
                ReferencePrice = company.ReferencePrice,
                DayVariation = company.DayVariation,
                EPS = company.EPS,
                PER = company.PER,
                Status = (int)company.Status
            };
        }
    }
}
