//using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Web.Services;

namespace LoanBrokerWebService
{
    /// <summary>
    /// Summary description for LoanBroker
    /// </summary>
    [WebService(Namespace = "http://loanbrokerservicethingybusiness.some/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class LoanBrokerService : System.Web.Services.WebService
    {
        /*
            TDK: Testing have shown that each client gets a new instance of the service, so there is no reason to handle async calls, as long as we are happy with the client not getting
            any status updates while waiting on the result
        */

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


            if (HandleMessaging.SendMessage<LoanBroker.model.LoanRequest>(Queues.LOANBROKER_IN, loanRequest))
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
                HostName = Queues.RABBITMQ_HOSTNAME
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Queues.LOANBROKER_OUT,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicQos(0, 1, false); // Get one at the time

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue: Queues.LOANBROKER_OUT,
                                         noAck: false,
                                         consumer: consumer);
                    bool weDontHaveIt = true;

                    int loopCount = 0;

                    while (weDontHaveIt)
                    {
                        //Looks like the items stays in the queue
                        BasicDeliverEventArgs ea = consumer.Queue.Dequeue();
                        LoanBroker.model.LoanResponse loanResponse = JsonConvert.DeserializeObject<LoanBroker.model.LoanResponse>(Encoding.UTF8.GetString(ea.Body));
                        if (loanRequest.SSN == loanResponse.SSN)
                        {
                            weDontHaveIt = false;
                            returnString = JsonConvert.SerializeObject(loanResponse);
                        }
                        else
                        {
                            // Old messages in the queue. Should use message TTL in aggregator
                            if (loopCount < 5000)
                            {
                                // Return it to the queue
                                // what is the difference between true and false?
                                channel.BasicReject(ea.DeliveryTag, false);
                                //channel.BasicReject(ea.DeliveryTag, true);
                            }
                            else
                                loopCount = 0;
                        }
                        loopCount++;

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
            using (IModel channel = new ConnectionFactory() { HostName = Queues.RABBITMQ_HOSTNAME }.CreateConnection().CreateModel())
            {
                channel.QueueDeclare(queue: Queues.LOANBROKER_OUT,
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
                channel.BasicConsume(queue: Queues.LOANBROKER_OUT,
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