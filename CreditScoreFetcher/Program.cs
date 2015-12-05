using CreditScoreInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using LoanBroker.model;

namespace CreditScoreFetcher
{
    public class Program
    {
        private static string QUEUE_OUT = "group1_creditbureau_out";
        private static string QUEUE_IN = "group1_loanbroker_in";

        static void Main(string[] args)
        {
            Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanRequest loanRequest;

                CreditScoreService.CreditScoreServiceClient service = new CreditScoreService.CreditScoreServiceClient();
                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);

                loanRequest.CreditScore = service.creditScore(loanRequest.SSN);

                Console.WriteLine("<--Enriched message content:");
                Console.WriteLine("<--" + loanRequest);

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                Utility.HandleMessaging.SendMessage<LoanRequest>(QUEUE_OUT, loanRequest);
            });
        }
    }
}