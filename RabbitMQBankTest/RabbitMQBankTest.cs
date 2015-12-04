using LoanBroker.model;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQBankTest
{
    class RabbitMQBankTest
    {
        static string SendQueueName = "group1_loanbroker_in";
        static string ReceiveQueueName = "group1_delegater_out";

        static void Main(string[] args)
        {
            Console.Write("-->Number of messages to send: ");
            int messages = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\n\t<--Started sending messages!");
            for (int i = 0; i < messages; i++)
            {
                Utility.HandleMessaging.SendMessage<LoanRequest>(SendQueueName, new LoanRequest() { Amount = 100, Duration = 12, SSN = "123456-7890" });
                Console.WriteLine("\t<--Messages sent: " + (i + 1) + "/" + messages);
            }
            Console.WriteLine("\t<--Stopped sending messages!\n");

            Console.Write("-->Press [Enter] to start receiving messages.");
            Console.ReadLine();

            Console.WriteLine("\n\t<--Started receiving messages!");

            var consumer = Utility.HandleMessaging.RecieveMessage(ReceiveQueueName, (object model, BasicDeliverEventArgs ea) =>
            {
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine("\t<--Message: " + message + " on queue: " + ea.RoutingKey);
            });
            //Utility.HandleMessaging.SendMessage<LoanRequest>("group1_loanbroker_in", new LoanRequest() { Amount = 100, Duration = 12, SSN = "123456-7890" });
        }
    }
}