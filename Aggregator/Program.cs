using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandleMessaging.RecieveMessage(Queues.NORMALIZER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.NORMALIZER_OUT);

                LoanResponse loanResponse;

                loanResponse = JsonConvert.DeserializeObject<LoanResponse>(Encoding.UTF8.GetString(ea.Body));

                // Do some sweet aggregating!

                Console.WriteLine("<--Sending message on queue: " + Queues.AGGREGATOR_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanResponse>(Queues.AGGREGATOR_OUT, loanResponse);
            });
        }
    }
}