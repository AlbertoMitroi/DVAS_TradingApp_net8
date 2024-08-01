using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InternshipTradingApp.CompanyInventory.Company.Company;

namespace InternshipTradingApp.CompanyInventory.Company.Features.Shared
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal ReferencePrice { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal ClosingPrice { get;  set; }
        public decimal PER { get; set; }
        public decimal DayVariation { get; set; }
        public decimal EPS { get; set; }
        public int Status { get; set; }
    }
}
