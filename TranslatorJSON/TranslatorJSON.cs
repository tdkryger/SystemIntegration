using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TranslatorJSON
{
    public class TranslatorJSON
    {
        static void Main(string[] args)
        {
            Console.Title = "Translator JSON";
            //if (args.Length < 1)
            //{
            //    Console.BackgroundColor = ConsoleColor.Red;
            //    Console.Error.WriteLine("Usage: {0} [routingkey]", Environment.GetCommandLineArgs()[0]);
            //    Console.BackgroundColor = ConsoleColor.Black;
            //    Console.WriteLine(" Press [enter] to exit.");

            //    Console.ReadLine();
            //    Environment.ExitCode = 1;
            //    return;
            //}

            string routingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQJSONBank;

            Console.WriteLine("<--Listening for messages on exchange: " + Queues.RULEBASEFETCHER_OUT + " with routing key: " + routingKey);

            HandleMessaging.RecieveMessage(Queues.RULEBASEFETCHER_OUT, string.Format("QUEUE_{0}", routingKey), routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + Queues.RULEBASEFETCHER_OUT);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();

                handleRabbitMQJSONBank(loanRequest);
            }
            );
        }

        private static void handleRabbitMQJSONBank(LoanRequest loanRequest)
        {
            //TODO: Hvad er exchangen cphbusiness.bankJSON?
            //SSN;CreditScore;Amount;Duration

            string msg = "{\"ssn\":" + loanRequest.SSN + ",\"creditScore\":" + loanRequest.CreditScore.ToString() +  
                ",\"loanAmount\":" + loanRequest.Amount.ToString() + ",\"loanDuration\":" + loanRequest.Duration +" }";

            //TDK: Throws an exception
            //string msg = string.Format(frmString, 
            //    loanRequest.SSN, 
            //    loanRequest.CreditScore.ToString(), 
            //    loanRequest.Amount.ToString(), 
            //    loanRequest.Duration.ToString()); 
            HandleMessaging.SendMessage("cphbusiness.bankJSON", Queues.RABBITMQJSONBANK_OUT, msg, "fanout");
        }
    }
}