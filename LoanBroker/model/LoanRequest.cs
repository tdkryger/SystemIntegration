using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBroker.model
{
    public class LoanRequest
    {
        public int CreditScore { get; set; }
        public string SSN { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public List<Bank> Banks { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}