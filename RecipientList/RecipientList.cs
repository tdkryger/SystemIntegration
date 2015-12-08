using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipientList
{
    class RecipientList
    {
        private static string QUEUE_IN = "group1_bankfetcher_out";
        private static string EXCHANGE_OUT = "group1_rulebasefetcher_out";

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
                Console.WriteLine();
                  

                /* 
                    We need Routing: https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html
                    That means an Exchange... So new methods to send and recieve..
                */

                if (loanRequest.Banks != null)
                {
                    foreach (Bank b in loanRequest.Banks)
                    {
                        //TDK: is this the best way to do Routing key? I think so. 
                        Utility.HandleMessaging.SendMessage<LoanRequest>(EXCHANGE_OUT, b.Id.ToString(), loanRequest);
                        Console.WriteLine("<--Sending message on exchange: " + EXCHANGE_OUT + " with routing key: " + b.Id.ToString());
                        Console.WriteLine();
                    }
                }   
            });
        }
    }
}
