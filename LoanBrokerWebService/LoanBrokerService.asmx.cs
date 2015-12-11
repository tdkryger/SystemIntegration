using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace LoanBrokerWebService
{
    /// <summary>
    /// Summary description for LoanBroker
    /// </summary>
    [WebService(Namespace = "http://loanbroker.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LoanBrokerService : System.Web.Services.WebService
    {
        /*
            TDK: Testing have shown that each client gets a new instance of the service, so there is no reason to handle async calls, as long as we are happy with the client not getting
            any status updates while waiting on the result
        */

        #region fields
        private static string QUEUE_IN = "group1_loanbroker_out";
        private static string QUEUE_OUT = "group1_loanbroker_in";
        private static string RABBITMQ_HOSTNAME = "datdb.cphbusiness.dk";
        #endregion

        #region Public Methods
        /// <summary>
        /// Get a loan quoute
        /// </summary>
        /// <param name="ssn">Social Security Number</param>
        /// <param name="amount">The amount to loan</param>
        /// <param name="duration">The duration in months</param>
        /// <returns>The loan response as JSON</returns>
        [WebMethod]
        public string GetLoanQuoute(string ssn, decimal amount, int duration)
        {
            LoanBroker.model.LoanRequest loanRequest = new LoanBroker.model.LoanRequest()
            {
                Amount = amount,
                Duration = duration,
                SSN = ssn
            };
            string returnString = "Could not send the message";

            if (LoanBroker.Utility.HandleMessaging.SendMessage<LoanBroker.model.LoanRequest>(QUEUE_OUT, loanRequest))
            {
                returnString = blockingRead(loanRequest);
            }
            return returnString;
        }
        #endregion

        #region Private Methods
        private string blockingRead(LoanBroker.model.LoanRequest loanRequest)
        {
            string returnString = "Could not send the message";
            var factory = new ConnectionFactory()
            {
                HostName = RABBITMQ_HOSTNAME
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_IN,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicQos(0, 1, false); // Get one at the time

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue: QUEUE_IN,
                                         noAck: false,
                                         consumer: consumer);
                    bool weDontHaveIt = true;

                    while (weDontHaveIt)
                    {
                        BasicDeliverEventArgs ea = consumer.Queue.Dequeue();
                        LoanBroker.model.LoanResponse loanResponse = JsonConvert.DeserializeObject<LoanBroker.model.LoanResponse>(Encoding.UTF8.GetString(ea.Body));
                        if (loanRequest.SSN == loanResponse.SSN)
                        {
                            weDontHaveIt = false;
                            returnString = JsonConvert.SerializeObject(loanResponse);
                        }
                        else
                        {
                            // Return it to the queue
                            channel.BasicReject(ea.DeliveryTag, true);
                        }

                    }
                }
            }
            return returnString;
        }

        [Obsolete("nonBlockingRead is still blocking, just somewhere else.. This is worse than the blockingRead, as this could sleep atleast 5ms. Please use blockingRead instead.")]
        private string nonBlockingRead(LoanBroker.model.LoanRequest loanRequest)
        {
            string returnString = "Could not send the message";

            EventingBasicConsumer consumer;
            using (IModel channel = new ConnectionFactory() { HostName = RABBITMQ_HOSTNAME }.CreateConnection().CreateModel())
            {
                channel.QueueDeclare(queue: QUEUE_IN,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                consumer = new EventingBasicConsumer(channel);

                bool weDontHaveIt = true;

                consumer.Received += (model, ea) =>
                {
                    LoanBroker.model.LoanResponse loanResponse = JsonConvert.DeserializeObject<LoanBroker.model.LoanResponse>(Encoding.UTF8.GetString(ea.Body));
                    if (loanResponse.SSN == loanRequest.SSN)
                    {
                        weDontHaveIt = false;
                    }
                    else
                    {
                    // Return it to the queue
                    channel.BasicReject(ea.DeliveryTag, true);
                    }

                };
                channel.BasicConsume(queue: QUEUE_IN,
                                     noAck: true,
                                     consumer: consumer);

                while (weDontHaveIt)
                {
                    System.Threading.Thread.Sleep(5);
                }
            }
            return returnString;
        }


        #endregion
    }
}
