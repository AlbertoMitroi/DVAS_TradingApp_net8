using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTradingApp.AccountManagement.DTOs
{
    public class BankAccountResponseDto
    {
        public int Id { get; set; }
        public string IBAN { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}
