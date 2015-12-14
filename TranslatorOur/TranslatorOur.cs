using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TranslatorOur
{
    public class TranslatorOur
    {
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

            Console.WriteLine("<--Listening for messages on exchange: " + Queues.RULEBASEFETCHER_OUT + " with routing key: " + routingKey);

            HandleMessaging.RecieveMessage(Queues.RULEBASEFETCHER_OUT, routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + Queues.RULEBASEFETCHER_OUT);

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
            HandleMessaging.SendMessage<string>(Queues.RABBITMQOURBANK_OUT, msg);
        }
    }
}