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
            Console.Title = "Translator Web";
            Console.SetWindowSize(80, 5);

            string routingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_WebServiceBank;

            Console.WriteLine("<--Listening for messages on exchange: " + Queues.RULEBASEFETCHER_OUT + " with routing key: " + routingKey);

            HandleMessaging.RecieveMessage(Queues.RULEBASEFETCHER_OUT, string.Format("QUEUE_{0}", routingKey), routingKey, (object model, BasicDeliverEventArgs ea) =>
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
            //HandleMessaging.SendMessage<decimal>(Queues.WEBSERVICEBANK_OUT, msg);
            LoanResponse loanResponse = new LoanResponse()
            {
                SSN = loanRequest.SSN,
                BankName = "Our Web Bank",
                InterestRate = msg
            };
            HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
        }
    }
}