using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace CreditScoreFetcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Credit Score Fetcher";
            Console.WriteLine("<--Listening for messages on queue: " + Queues.LOANBROKER_IN);
            HandleMessaging.RecieveMessage(Queues.LOANBROKER_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.LOANBROKER_IN);

                LoanRequest loanRequest;

                CreditScoreService.CreditScoreServiceClient service = new CreditScoreService.CreditScoreServiceClient();
                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);

                loanRequest.CreditScore = service.creditScore(loanRequest.SSN);

                Console.WriteLine("<--Enriched message content:");
                Console.WriteLine("<--" + loanRequest);

                Console.WriteLine("<--Sending message on queue: " + Queues.CREDITBUREAU_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanRequest>(Queues.CREDITBUREAU_OUT, loanRequest);
            });
        }
    }
}