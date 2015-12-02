using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceBank
{
    /// <summary>
    /// Summary description for WebServiceBank
    /// </summary>
    [WebService(Namespace = "http://SharkLoans.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceBank : System.Web.Services.WebService
    {
        [WebMethod]
        public double ProcessLoanRequest(string ssn, int creditScore, double amount, int duration)
        {
            return SimpleBank.Bank.ProcessLoanRequest(ssn, creditScore, amount, duration);
        }
    }
}
