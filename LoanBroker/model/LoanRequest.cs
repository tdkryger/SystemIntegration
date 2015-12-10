using Newtonsoft.Json;
using System.Collections.Generic;

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