using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQOurBankNormalizer
{
    public class RabbitMQOurBankNormalizer
    {
        public static void Main(string[] args)
        {
            Console.Title = "RabbitMQOurBankNormalizer";
            Console.SetWindowSize(80, 5);
            HandleMessaging.RecieveMessage(Queues.RABBITMQOURBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQOURBANK_OUT);

                LoanResponse loanResponse;

                string s = Encoding.UTF8.GetString(ea.Body);

                //TODO: TDK: Error here.. Gets a loan request and not a ourbankresponse
                loanResponse = JsonConvert.DeserializeObject<LoanResponse>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
            });
        }


    }
}