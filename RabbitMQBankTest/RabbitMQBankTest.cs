using LoanBroker.model;
using LoanBroker.Utility;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQBankTest
{
    public class RabbitMQBankTest
    {
        static void Main(string[] args)
        {
            Console.Write("-->Number of messages to send: ");
            int messages = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\n\t<--Started sending messages!");
            for (int i = 0; i < messages; i++)
            {
                HandleMessaging.SendMessage<LoanRequest>(Queues.LOANBROKER_IN, new LoanRequest()
                {
                    Amount = 100,
                    Duration = 12,
                    SSN = "123456-7890"
                });
                Console.WriteLine("\t<--Messages sent: " + (i + 1) + "/" + messages);
            }
            Console.WriteLine("\t<--Stopped sending messages!\n");

            Console.Write("-->Press [Enter] to start receiving messages.");
            Console.ReadLine();

            Console.WriteLine("\n\t<--Started receiving messages!");

            var consumer = HandleMessaging.RecieveMessage(Queues.DELEGATER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                byte[] body = ea.Body;
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine("\t<--Message: " + message + " on queue: " + ea.RoutingKey);
            });
            //Utility.HandleMessaging.SendMessage<LoanRequest>("group1_loanbroker_in", new LoanRequest() { Amount = 100, Duration = 12, SSN = "123456-7890" });
        }
    }
}