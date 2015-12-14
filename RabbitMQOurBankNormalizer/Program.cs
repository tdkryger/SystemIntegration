using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQOurBankNormalizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandleMessaging.RecieveMessage(Queues.RABBITMQOURBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQOURBANK_OUT);

                LoanResponse loanResponse;
                OurBankResponse bankResponse;

                bankResponse = JsonConvert.DeserializeObject<OurBankResponse>(Encoding.UTF8.GetString(ea.Body));
                loanResponse = new LoanResponse()
                {
                    InterestRate = bankResponse.InterestRate,
                    SSN = bankResponse.SSN,
                    BankName = bankResponse.Name
                };

                Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
            });
        }

        private class OurBankResponse
        {
            public string Name { get; set; }
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}