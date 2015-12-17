using LoanBroker.model;
using LoanBroker.Utility;
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
        public static void Main(string[] args)
        {
            Console.Title = "2 - RuleBase Fetcher";
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(80, 5);
            Console.WriteLine("<--Listening for messages on queue: " + Queues.CREDITBUREAU_OUT);
            HandleMessaging.RecieveMessage(Queues.CREDITBUREAU_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.CREDITBUREAU_OUT);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                
                RuleBaseServiceSoapClient service;
                ArrayOfString strings;

                service = new RuleBaseServiceSoapClient();
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

                Console.WriteLine("<--Sending message on queue: " + Queues.BANKFETCHER_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanRequest>(Queues.BANKFETCHER_OUT, loanRequest);
            });
        }
    }
}