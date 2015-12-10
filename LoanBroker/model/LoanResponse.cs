using Newtonsoft.Json;

namespace LoanBroker.model
{
    public class LoanResponse
    {
        public decimal InterestRate { get; set; }
        public string SSN { get; set; }
        public string BankName { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}