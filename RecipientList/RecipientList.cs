using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RecipientList
{
    public class RecipientList
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("<--Listening for messages on queue: " + Queues.BANKFETCHER_OUT);
            HandleMessaging.RecieveMessage(Queues.BANKFETCHER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.BANKFETCHER_OUT);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();
                  

                /* 
                    We need Routing: https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html
                    That means an Exchange... So new methods to send and recieve..
                */

                if (loanRequest.Banks != null)
                {
                    foreach (Bank bank in loanRequest.Banks)
                    {
                        //TDK: is this the best way to do Routing key? I think so. 
                        HandleMessaging.SendMessage<LoanRequest>(Queues.RULEBASEFETCHER_OUT, bank.RoutingKey, loanRequest);
                        Console.WriteLine("<--Sending message on exchange: " + Queues.RULEBASEFETCHER_OUT + " with routing key: " + bank.RoutingKey);
                        Console.WriteLine();
                    }
                }   
            });
        }
    }
}