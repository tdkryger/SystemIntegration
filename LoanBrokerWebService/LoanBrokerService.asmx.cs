using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace LoanBrokerWebService
{
    /// <summary>
    /// Summary description for LoanBroker
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoanBrokerService : System.Web.Services.WebService
    {
        #region fields
        private static string QUEUE_IN = "group1_loanbroker_out";
        private static string QUEUE_OUT = "group1_loanbroker_in";

        private Dictionary<string, LoanQuoute> _loanQuotes = new Dictionary<string, LoanQuoute>();
        System.ComponentModel.BackgroundWorker _bgWorker;
        #endregion

        #region Internal stuff
        private class LoanQuoute
        {
            public LoanBroker.model.LoanRequest LoanRequest { get; set; }
            public decimal LoanResponse { get; set; } // replace with LoanResponse
            public bool Done { get; set; }
        }
        #endregion

        #region Construcor
        public LoanBrokerService()
        {
            // Start a background thread / task that listens on QUEUE_IN
            _bgWorker = new System.ComponentModel.BackgroundWorker();
            _bgWorker.DoWork += _bgWorker_DoWork;
        }

        private void _bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach(var ii in _loanQuotes)
            {
                if (ii.Value.Done)
                {
                    _loanQuotes.Remove(ii.Key);

                }
            }
        }
        #endregion

        /// <summary>
        /// Get a loan quoute
        /// </summary>
        /// <param name="ssn">Social Security Number</param>
        /// <param name="amount">The amount to loan</param>
        /// <param name="duration">The duration in months</param>
        /// <returns>The interest rate</returns>
        [WebMethod]
        public decimal GetLoanQuoute(string ssn, decimal amount, int duration)
        {
            LoanQuoute lq = new LoanQuoute()
            {
                LoanRequest = new LoanBroker.model.LoanRequest()
                {
                    Amount = amount,
                    Duration = duration,
                    SSN = ssn
                },
                Done = false,
                LoanResponse = decimal.Zero
            };
            _loanQuotes.Add(ssn, lq);
            sendRequest(lq.LoanRequest);

            /*
                In a thread/task:
                    *  Create a LoanRequest
                    *  Add LoanRequest to a list..
                    *  Send the LoanRequest into the system
                    

            */

            throw new NotImplementedException();
        }

        protected Task<decimal> sendRequest(LoanBroker.model.LoanRequest loanRequest)
        {


            LoanBroker.Utility.HandleMessaging.SendMessage<LoanBroker.model.LoanRequest>(QUEUE_OUT, loanRequest);
            //LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            //{
            //    Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

            //    LoanRequest loanRequest;

            //    loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

            //    Console.WriteLine("<--Message content:");
            //    Console.WriteLine("<--" + loanRequest);
            //    Console.WriteLine();
            //});
            //}
            throw new NotImplementedException();
        }
    }
}
