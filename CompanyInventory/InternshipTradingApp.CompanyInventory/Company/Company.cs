using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Company
{
    public class Company
    {
        public enum CompanyStatus 
        { 
            OnTheMarket = 0, 
            Suspended = 1 
        }
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;
        public decimal Price { get; private set; } 
        public decimal ReferencePrice { get; private set; }
        public decimal OpeningPrice { get; private set; }
        public decimal ClosingPrice { get; private set; }
        public decimal PER => EPS != 0 ? Price / EPS : 0;
        public decimal DayVariation => ReferencePrice != 0 ? (Price - ReferencePrice) / ReferencePrice * 100 : 0;
        public decimal EPS { get; private set; }
        public CompanyStatus Status { get; private set; }

        public static Company Create(string name, 
                                     string symbol, 
                                     decimal price, 
                                     decimal openingPrice, 
                                     decimal closingPrice,
                                     decimal referencePrice,
                                     decimal eps)
        { 
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException("symbol");
            
            return new Company 
            {
                Name = name,
                Symbol = symbol,
                Price = price,
                OpeningPrice = openingPrice,
                ClosingPrice = closingPrice,                                
                ReferencePrice = referencePrice,
                EPS = eps
            }; 
        
        }
        public void UpdateTradingData(Company newCompanyData)
        { 
           //TBD: validate price variation
            Price = newCompanyData.Price;
            ReferencePrice = newCompanyData.ReferencePrice;  
            OpeningPrice = newCompanyData.OpeningPrice;
            ClosingPrice = newCompanyData.ClosingPrice;
            EPS = newCompanyData.EPS;
        }

        public void UpdateTradingStatus(CompanyStatus newStatus)
        {
            //TBD validations for status change
            Status = newStatus;
        }

        public void UpdatePrice(decimal newPrice)
        { 
            //TBD: validate against price variation
            Price = newPrice;
        }
    }
}
