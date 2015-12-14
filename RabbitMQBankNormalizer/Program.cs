using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQJSONBankNormalizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandleMessaging.RecieveMessage(Queues.RABBITMQJSONBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQJSONBANK_OUT);

                LoanResponse loanResponse;
                JsonBankResponse bankResponse;

                bankResponse = JsonConvert.DeserializeObject<JsonBankResponse>(Encoding.UTF8.GetString(ea.Body));
                loanResponse = new LoanResponse()
                {
                    InterestRate = bankResponse.InterestRate,
                    SSN = bankResponse.SSN,
                    BankName = ea.RoutingKey.Split('_')[1] // Gets the bank name from the queue name
                };

                Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
            });
        }

        private class JsonBankResponse
        {
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}