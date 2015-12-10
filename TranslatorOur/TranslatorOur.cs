using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TranslatorOur
{
    class TranslatorOur
    {
        private static string EXCHANGE_IN = "group1_rulebasefetcher_out";
        private static string QUEUE_OUT = "group1_rabbitmqourbank_out";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Usage: {0} [routingkey]", Environment.GetCommandLineArgs()[0]);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(" Press [enter] to exit.");

                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            string routingKey = args[0];

            Console.WriteLine("<--Listening for messages on exchange: " + EXCHANGE_IN + " with routing key: " + routingKey);

            LoanBroker.Utility.HandleMessaging.RecieveMessage(EXCHANGE_IN, routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + EXCHANGE_IN);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();

                handleRabbitMQOurBank(loanRequest);
            }
            );
        }

        private static void handleRabbitMQOurBank(LoanRequest loanRequest)
        {
            //SSN;CreditScore;Amount;Duration
            string msg = string.Format("{0};{1};{2};{3}", loanRequest.SSN, loanRequest.CreditScore, loanRequest.Amount, loanRequest.Duration);
            LoanBroker.Utility.HandleMessaging.SendMessage<string>(QUEUE_OUT, msg);
        }
    }
}
