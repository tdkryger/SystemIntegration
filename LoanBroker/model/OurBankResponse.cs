using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBroker.model
{
    public class OurBankResponse
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public decimal InterestRate { get; set; }
    }
}
