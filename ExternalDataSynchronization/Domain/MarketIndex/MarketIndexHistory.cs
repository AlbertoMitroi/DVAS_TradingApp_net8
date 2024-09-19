using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataSynchronization.Domain.MarketIndex
{
    public class MarketIndexHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
