using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RuleBaseFetcher.RuleBaseService;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBaseFetcher
{
    public class Program
    {
        private static string QUEUE_IN = "group1_creditbureau_out";
        private static string QUEUE_OUT = "group1_bankfetcher_out";

        public static void Main(string[] args)
        {
            Console.WriteLine("<--Listening for messages on queue: " + QUEUE_IN);
            LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                
                RuleBaseService.RuleBaseServiceSoapClient service;
                ArrayOfString strings;

                service = new RuleBaseService.RuleBaseServiceSoapClient();
                loanRequest.Banks = new List<Bank>();
                strings = service.GetBanks();

                foreach (string jSonRepOfBank in strings)
                {
                    Bank bank;
                    bank = JsonConvert.DeserializeObject<Bank>(jSonRepOfBank);

                    if (loanRequest.CreditScore >= bank.MinCreditScore &&
                        loanRequest.CreditScore <= bank.MaxCreditScore &&
                        loanRequest.Amount >= bank.MinAmount &&
                        loanRequest.Amount <= bank.MaxAmount &&
                        loanRequest.Duration >= bank.MinDuration &&
                        loanRequest.Duration <= bank.MaxDuration)
                    {
                        loanRequest.Banks.Add(bank);
                    }
                }

                Console.WriteLine("<--Enriched message content:");
                Console.WriteLine("<--" + loanRequest);

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                LoanBroker.Utility.HandleMessaging.SendMessage<LoanRequest>(QUEUE_OUT, loanRequest);
            });
        }
    }
}