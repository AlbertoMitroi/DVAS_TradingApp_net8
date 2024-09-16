using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.CompanyInventory.Features.Query
{
    public class GetTopXCompaniesQuery
    {
        public int? X { get; set; }
        public string? Value { get; set; }
        public string OrderToggle { get; set; }
    }
}
