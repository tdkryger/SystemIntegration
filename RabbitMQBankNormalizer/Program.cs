using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQJSONBankNormalizer
{
    public class Program
    {
        private static string QUEUE_IN = "group1_rabbitmqjsonbank_out";
        private static string QUEUE_OUT = "group1_normalizer_out";

        public static void Main(string[] args)
        {
            LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanResponse loanResponse;
                JsonBankResponse bankResponse;

                bankResponse = JsonConvert.DeserializeObject<JsonBankResponse>(Encoding.UTF8.GetString(ea.Body));
                loanResponse = new LoanResponse()
                {
                    InterestRate = bankResponse.InterestRate,
                    SSN = bankResponse.SSN,
                    BankName = ea.RoutingKey.Split('_')[1] // Gets the bank name from the queue name
                };

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                LoanBroker.Utility.HandleMessaging.SendMessage<LoanResponse>(QUEUE_OUT, loanResponse);
            });
        }

        private class JsonBankResponse
        {
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}