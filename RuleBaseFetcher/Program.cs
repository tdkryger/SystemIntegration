using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBaseFetcher
{
    public class Program
    {
        private static string QUEUE_IN = "group1_creditbureau_out";
        private static string QUEUE_OUT = "group1_bankfetcher_out";

        public static void Main(string[] args)
        {
            Console.WriteLine("<--Listening for messages on queue: " + QUEUE_IN);
            Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);

                // Add enrichment code beneath here


                Console.WriteLine("<--Enriched message content:");
                Console.WriteLine("<--" + loanRequest);

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                Utility.HandleMessaging.SendMessage<LoanRequest>(QUEUE_OUT, loanRequest);
            });
        }
    }
}