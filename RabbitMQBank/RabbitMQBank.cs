using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Resources;

namespace RabbitMQBank
{
    class RabbitMQBank
    {

        static string QUEUE_OUT = "group1_bank_out";
        static string QUEUE_IN = "group1_delegater_out";

        static void Main(string[] args)
        {
            Console.WriteLine("<--Listening for messages on queue: " + QUEUE_IN);
            LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                byte[] inBody = ea.Body;
                string message = Encoding.UTF8.GetString(inBody);

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + message);

                //SSN;CreditScore;Amount;Duration
                string[] parts = message.Split(';');
                string ssn = parts[0];
                int creditScore = 0;
                int.TryParse(parts[1], out creditScore);
                decimal amount = 0;
                decimal.TryParse(parts[2], out amount);
                int duration = 0;
                int.TryParse(parts[3], out duration);

                decimal sendMessage = SimpleBank.Bank.ProcessLoanRequest(ssn, creditScore, amount, duration);

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT + " > " + sendMessage.ToString());
                Console.WriteLine();

                LoanBroker.Utility.HandleMessaging.SendMessage<decimal>(QUEUE_OUT, sendMessage);
            });

        }
    }
}
