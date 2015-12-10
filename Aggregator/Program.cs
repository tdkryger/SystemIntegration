using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Aggregator
{
    public class Program
    {
        private static string QUEUE_IN = "group1_normalizer_out";
        private static string QUEUE_OUT = "group1_aggregator_out";

        public static void Main(string[] args)
        {
            LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanResponse loanResponse;

                loanResponse = JsonConvert.DeserializeObject<LoanResponse>(Encoding.UTF8.GetString(ea.Body));

                // Do some sweet aggregating!

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                LoanBroker.Utility.HandleMessaging.SendMessage<LoanResponse>(QUEUE_OUT, loanResponse);
            });
        }
    }
}