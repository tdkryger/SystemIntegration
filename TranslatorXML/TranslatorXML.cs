using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TranslatorXML
{
    class TranslatorXML
    {
        private static string EXCHANGE_IN = "group1_rulebasefetcher_out";
        private static string QUEUE_OUT = "group1_rabbitmqxmlbank_out";

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

                handleRabbitMQXMLBank(loanRequest);
            });
        }

        private static void handleRabbitMQXMLBank(LoanRequest loanRequest)
        {
            DateTime dtDuration = new DateTime(1970, 1, 1).AddMonths(loanRequest.Duration);
            string msg = string.Format("<LoanRequest><ssn>{0}</ssn><creditScore>{1}</creditScore><loanAmount>{2}</loanAmount><loanDuration>{3}</loanDuration></LoanRequest>",
                loanRequest.SSN,
                loanRequest.CreditScore,
                loanRequest.Amount,
                dtDuration.ToString("yyyy-MM-dd HH:mm:ss:ff CET") // since we dont care about hours and so on, time zone info is useless
                );

            LoanBroker.Utility.HandleMessaging.SendMessage("cphbusiness.bankXML", QUEUE_OUT, msg, "fanout");
        }
    }
}
