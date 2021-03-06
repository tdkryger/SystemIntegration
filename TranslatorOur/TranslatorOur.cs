﻿using LoanBroker.model;
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
            Console.Title = "Translator Our";
            Console.SetWindowSize(80, 5);

            string routingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQOURBank;
            
            Console.WriteLine("<--Listening for messages on exchange: " + Queues.DELEGATER_OUT + " with routing key: " + routingKey);

            HandleMessaging.RecieveMessage(Queues.RULEBASEFETCHER_OUT, string.Format("QUEUE_{0}", routingKey), routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + Queues.DELEGATER_OUT);

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
            HandleMessaging.SendMessage<string>(Queues.RABBITMQOURBANK_IN, msg);
        }
    }
}