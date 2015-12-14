using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TranslatorWeb
{
    public class TranslatorWeb
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

                handleWebServiceBank(loanRequest);
            }
            );
        }

        private static void handleWebServiceBank(LoanRequest loanRequest)
        {
            WebServiceBank.WebServiceBank webBank = new WebServiceBank.WebServiceBank();
            decimal msg = webBank.ProcessLoanRequest(loanRequest.SSN, loanRequest.CreditScore, loanRequest.Amount, loanRequest.Duration);
            //TODO: Send loanrequest info aswell as decimal msg
            HandleMessaging.SendMessage<decimal>(Queues.WEBSERVICEBANK_OUT, msg);
        }
    }
}