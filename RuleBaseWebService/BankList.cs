using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBaseWebService
{
    // not used
    public sealed class BankList
    {
        static readonly BankList instance = new BankList();

        public List<LoanBroker.model.Bank> Banks { get; private set; }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BankList()
        {
        }

        BankList()
        {
            Banks = new List<LoanBroker.model.Bank>();
        }

        public static BankList Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
