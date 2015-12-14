using LoanBroker.Utility;
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
    public class WebServiceBank : WebService
    {
        [WebMethod]
        public decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration)
        {
            return BankingUtility.ProcessLoanRequest(ssn, creditScore,  amount, duration);
        }
    }
}