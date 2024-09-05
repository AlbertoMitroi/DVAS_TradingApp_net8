using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.ModuleIntegration.CompanyInventory
{
    public class CompanyWithHistoryGetDTO
    {
        public CompanyGetDTO Company { get; set; }
        public List<CompanyHistoryGetDTO> History { get; set; }
    }
}
