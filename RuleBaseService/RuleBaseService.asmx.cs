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