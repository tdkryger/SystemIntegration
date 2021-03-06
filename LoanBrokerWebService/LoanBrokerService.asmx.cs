﻿//using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Timers;
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

        private const long TIMEOUT = 60000;

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
            if (ssn.Length != 11)
                throw new ArgumentException("SSN must be in the format 'xxxxxx-xxxx'");
            if (ssn.Substring(6, 1) != "-")
                throw new ArgumentException("SSN must be in the format 'xxxxxx-xxxx'");

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0");
            if (duration <= 0)
                throw new ArgumentException("Duration must be greater than 0");

            LoanBroker.model.LoanRequest loanRequest = new LoanBroker.model.LoanRequest()
            {
                Amount = amount,
                Duration = duration,
                SSN = ssn
            };
            string returnString = "Could not send the message";

            // Do some stupid looping and "delete" old messages?


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

                    channel.BasicQos(0, 1, false); // Get one at the time.

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queue: Queues.LOANBROKER_OUT,
                                         noAck: false,
                                         consumer: consumer);
                    bool weDontHaveIt = true;
                    using (Timer _timeOutTimer = new Timer(TIMEOUT))
                    {
                        _timeOutTimer.Enabled = true;
                        _timeOutTimer.Elapsed += _timeOutTimer_Elapsed;

                        while (weDontHaveIt)
                        {
                            //Looks like the items stays in the queue
                            BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                            LoanBroker.model.LoanResponse loanResponse = JsonConvert.DeserializeObject<LoanBroker.model.LoanResponse>(Encoding.UTF8.GetString(ea.Body));
                            if (loanRequest.SSN == loanResponse.SSN)
                            {
                                weDontHaveIt = false;
                                returnString = JsonConvert.SerializeObject(loanResponse);
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            else
                            {
                                // Return it to the queue
                                // what is the difference between true and false?
                                //channel.BasicReject(ea.DeliveryTag, false);
                                //channel.BasicReject(ea.DeliveryTag, true);
                            }
                        }
                    }
                }
            }
            return returnString;
        }

        private void _timeOutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new TimeoutException("Timeout while waiting for response from loan broker");
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