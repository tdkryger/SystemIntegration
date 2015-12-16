using LoanBroker.model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace RuleBaseService
{
    /// <summary>
    /// Summary description for RuleBaseService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RuleBaseService : System.Web.Services.WebService
    {
        #region Private fields
        // Static gets reset sometimes.. Need something better..
        // Should use a HttpApplicationState Class (apparently) and a singleton..
        private static List<string> _banks = new List<string>();
        #endregion

        public RuleBaseService() : base()
        {
            Bank bank0, bank1, bank2, bank3;

            bank0 = new Bank()
            {
                Id = 0,
                Name = "Bank0",
                RoutingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQJSONBank
            };

            bank1 = new Bank()
            {
                Id = 1,
                Name = "Bank1",
                MinAmount = 123,
                RoutingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQOURBank
            };

            bank2 = new Bank()
            {
                Id = 2,
                Name = "Bank2",
                MinCreditScore = 678,
                RoutingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQXMLBank
            };

            bank3 = new Bank()
            {
                Id = 3,
                Name = "Bank3",
                MinAmount = 100,
                MinDuration = 12,
                MinCreditScore = 234,
                RoutingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_WebServiceBank
            };

            AddABank(JsonConvert.SerializeObject(bank0));
            AddABank(JsonConvert.SerializeObject(bank1));
            AddABank(JsonConvert.SerializeObject(bank2));
            AddABank(JsonConvert.SerializeObject(bank3));
        }

        #region private methods
        //Dunno if this works..
        private void getPersistentList()
        {
            HttpApplicationState applicationState = HttpContext.Current.Application;
            if (applicationState["BankList"] != null)
            {
                _banks = (List<string>)applicationState["BankList"];
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
        public void AddABank(string jSonRepOfBank)
        {
            _banks.Add(jSonRepOfBank);
            setPersistentList();
        }

        /// <summary>
        /// Removes a bank
        /// </summary>
        /// <param name="bank">The bank to remove</param>
        [WebMethod]
        public void RemoveABank(string jSonRepOfBank)
        {
            try
            {
                _banks.Remove(jSonRepOfBank);
                setPersistentList();
            }
            catch
            {
                // Just throw it away.. we don't care at the momemt... Should go to the error log on the server..
            }
        }

        /// <summary>
        /// Returns the list of banks
        /// The Rule Base Fetcher have to add all the banks if the return list is empty, and make the request again
        /// </summary>
        /// <returns>A list of banks</returns>
        [WebMethod]
        public string[] GetBanks()
        {
            getPersistentList();
            return _banks.ToArray();
        }
    }
}