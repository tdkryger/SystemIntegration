using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBroker
{
    abstract class TranslatorBase
    {
        #region Public methods
        public void SendMessage(string routingKey, string exchangeName, string queueName)
        {
            Console.WriteLine("<--Listening for messages on exchange: " + exchangeName + " with routing key: " + routingKey);

            LoanBroker.Utility.HandleMessaging.RecieveMessage(exchangeName, queueName, routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + exchangeName);

                model.LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<model.LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();

                handleBank(loanRequest);
            }
            );
        }

        #endregion

        #region protected methods
        protected abstract void handleBank(model.LoanRequest loanRequest);
        #endregion
    }
}
