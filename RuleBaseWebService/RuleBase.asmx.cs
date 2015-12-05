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
        // Static gets reset sometimes.. Need something better..
        // Should use a HttpApplicationState Class (apparently) and a singleton..
        private static List<LoanBroker.model.Bank> _banks = new List<LoanBroker.model.Bank>();
        #endregion

        #region private methods
        //Dunno if this works..
        private void getPersistentList()
        {
            HttpApplicationState applicationState = HttpContext.Current.Application;
            if (applicationState["BankList"] != null)
            {
                _banks = (List<LoanBroker.model.Bank>)applicationState["BankList"];
            }
        }

        private void setPersistentList()
        {
            HttpApplicationState applicationState = HttpContext.Current.Application;
            applicationState["BankList"] = _banks;
        }

        #endregion

        /// <summary>
        /// Add a bank to the available banks. No check for unique-ness
        /// </summary>
        /// <param name="bank">The bank to add</param>
        [WebMethod]
        public void AddABank(LoanBroker.model.Bank bank)
        {
            _banks.Add(bank);
            setPersistentList();
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
                setPersistentList();
            }
            catch
            {
                // Just throw it away.. we don't care at the momemt... Should go to the error log on the server..
            }
        }

        /// <summary>
        /// Creates a list of banks
        /// Right now the rule is random..
        /// The Rule Base Fetcher have to add all the banks if the return list is empty, and make the request again
        /// </summary>
        /// <returns>A list of banks, containing at least 1 bank, if we have any</returns>
        [WebMethod]
        public List<LoanBroker.model.Bank> GetBanks(decimal amount, int creditScore, int duration, string ssn)
        {
            LoanBroker.model.LoanRequest loanRequest = new LoanBroker.model.LoanRequest()
            {
                Amount=amount,
                CreditScore= creditScore,
                Duration=duration,
                SSN=ssn
            };
            if (_banks.Count == 0)
                getPersistentList();
            Random rnd = new Random();
            List<LoanBroker.model.Bank> banks = new List<LoanBroker.model.Bank>();
            foreach (LoanBroker.model.Bank b in _banks)
            {
                //TODO: The rule goes here... use the loanRequest obkject..

                if (rnd.Next(0, 2) > 0) // rnd.Next(0, 1) only returns 0, as 1 is excluded max (says tooltip)
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
