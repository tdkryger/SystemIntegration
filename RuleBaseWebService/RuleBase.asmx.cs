using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RuleBaseWebService
{
    /// <summary>
    /// Summary description for RuleBase
    /// </summary>
    [WebService(Namespace = "http://WorldOfRule.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RuleBase : System.Web.Services.WebService
    {
        #region Private fields
        private static List<LoanBroker.model.Bank> _banks = new List<LoanBroker.model.Bank>();
        #endregion

        /// <summary>
        /// Add a bank to the available banks. No check for unique-ness
        /// </summary>
        /// <param name="bank">The bank to add</param>
        [WebMethod]
        public void AddABank(LoanBroker.model.Bank bank)
        {
            _banks.Add(bank);
        }

        /// <summary>
        /// Removes a bank
        /// </summary>
        /// <param name="bank">The bank to remove</param>
        [WebMethod]
        public void RemoveABank(LoanBroker.model.Bank bank)
        {
            try
            {
                _banks.Remove(bank);
            }
            catch
            {
                // Just throw it away.. we don't care at the momemt... Should go to the error log on the server..
            }
        }

        /// <summary>
        /// Creates a list of banks
        /// Right now the rule is random..
        /// </summary>
        /// <returns>A list of banks, containing at least 1 bank, if we have any</returns>
        [WebMethod]
        public List<LoanBroker.model.Bank> GetBanks()
        {
            Random rnd = new Random();
            List<LoanBroker.model.Bank> banks = new List<LoanBroker.model.Bank>();
            foreach(LoanBroker.model.Bank b in _banks)
            {
                //TODO: The rule goes here...
                
                if (rnd.Next(0, 1) == 1)
                {
                    banks.Add(b);
                }
            }
            // Make sure we atleast a bank, if we have any banks to choose from..
            if (banks.Count == 0 && _banks.Count > 0)
            {
                int idx = rnd.Next(0, _banks.Count - 1);
                try
                {
                    banks.Add(_banks[idx]);
                }
                catch
                {
                    banks.Add(_banks[0]);
                }
            }
            return banks;
        }
    }
}
