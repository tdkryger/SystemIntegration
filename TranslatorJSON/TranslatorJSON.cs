using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Globalization;
using System.Text;

namespace TranslatorJSON
{
    public class TranslatorJSON
    {
        static void Main(string[] args)
        {
            Console.Title = "Translator JSON";
            Console.SetWindowSize(80, 5);

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

                handleRabbitMQJSONBank(loanRequest, routingKey);
            }
            );
        }

        private static void handleRabbitMQJSONBank(LoanRequest loanRequest, string routingKey)
        {
            /*
            String format from pdf:
            string msg = "{\"ssn\":" + loanRequest.SSN + ",\"creditScore\":" + loanRequest.CreditScore.ToString() +  
                ",\"loanAmount\":" + loanRequest.Amount.ToString() + ",\"loanDuration\":" + loanRequest.Duration +" }";

            but getting this from the bank:

            Exception: Something went wrong.Data should be sent like: { "ssn":1605789787,"loanAmount":10.0,"loanDuration":360,"rki":false}
            Can not instantiate value of type[simple type, class dk.cphbusiness.si.banktemplate.JsonDTO.BankLoanDTO] from JSON String; no single-String constructor/factory method

            */
            //string msg = "{ \"ssn\":" + loanRequest.SSN
            //    + ",\"loanAmount\":" + loanRequest.Amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"))
            //    + ",\"loanDuration\":" + loanRequest.Duration.ToString()
            //    + ",\"rki\":false }";
            

            //TODO: 18/12/2015 A java group says we have to send a json object...
            string msg = "{ \"ssn\":" + loanRequest.SSN.Replace("-", "")
                + ",\"creditScore\":"  + loanRequest.CreditScore.ToString()
                + ",\"loanAmount\":" + loanRequest.Amount.ToString(CultureInfo.CreateSpecificCulture("en-GB")) 
                + ",\"loanDuration\":" + loanRequest.Duration + " }";

            Console.WriteLine("--> Sending " + msg + " to cphbusiness.bankJSON");
            HandleMessaging.SendMessage("cphbusiness.bankJSON", routingKey, Queues.RABBITMQJSONBANK_OUT, msg, "fanout");
        }
    }
}