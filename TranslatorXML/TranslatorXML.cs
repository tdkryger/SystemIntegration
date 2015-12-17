using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Globalization;
using System.Text;

namespace TranslatorXML
{
    public class TranslatorXML
    {
        static void Main(string[] args)
        {
            Console.Title = "Translator XML";
            Console.SetWindowSize(80, 5);


            string routingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQXMLBank;

            Console.WriteLine("<--Listening for messages on exchange: " + Queues.RULEBASEFETCHER_OUT + " with routing key: " + routingKey);



            HandleMessaging.RecieveMessage(Queues.RULEBASEFETCHER_OUT, string.Format("QUEUE_{0}", routingKey), routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + Queues.RULEBASEFETCHER_OUT);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();

                handleRabbitMQXMLBank(loanRequest, routingKey);
            });
        }

        private static void handleRabbitMQXMLBank(LoanRequest loanRequest, string routingKey)
        {
            DateTime dtDuration = new DateTime(1970, 1, 1).AddMonths(loanRequest.Duration);

            /*

            Exception: Something went wrong. Data should be sent like: 
            <LoanRequest>   <ssn>12345678</ssn>   <creditScore>685</creditScore>   <loanAmount>10.0</loanAmount>   <loanDuration>1970-01-01 01:00:00.0 CET</loanDuration> </LoanRequest> 	ParseError at [row,col]:[1,1]
Message: Content is not allowed in prolog.

            */
            string msg = string.Format("<LoanRequest><ssn>{0}</ssn><creditScore>{1}</creditScore><loanAmount>{2}</loanAmount><loanDuration>{3}</loanDuration></LoanRequest>",
                loanRequest.SSN,
                loanRequest.CreditScore.ToString(),
                loanRequest.Amount.ToString(CultureInfo.CreateSpecificCulture("en-GB")),
                dtDuration.ToString("yyyy-MM-dd HH:mm:ss:ff CET") // since we dont care about hours and so on, time zone info is useless
                );
            //string msg = "<LoanRequest>   <ssn>12345678</ssn>   <creditScore>685</creditScore>   <loanAmount>10.0</loanAmount>   <loanDuration>1970-01-01 01:00:00.0 CET</loanDuration> </LoanRequest>";
            HandleMessaging.SendMessage("cphbusiness.bankXML" , string.Empty, Queues.RABBITMQXMLBANK_OUT, msg, "fanout");
        }
    }
}