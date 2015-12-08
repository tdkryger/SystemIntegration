using LoanBroker.model;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalTranslator
{
    class UniversalTranslator
    {
        private static string EXCHANGE_IN = "group1_rulebasefetcher_out";
        private static string QUEUE_OUT = "group1_delegater_out";

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Usage: {0} [routingkey]", Environment.GetCommandLineArgs()[0]);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                Environment.ExitCode = 1;
                return;
            }

            string routingKey = args[0];

            Console.WriteLine("<--Listening for messages on exchange: " + EXCHANGE_IN + " with routing key: " + routingKey);

            Utility.HandleMessaging.RecieveMessage(EXCHANGE_IN, routingKey, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on exchange: " + EXCHANGE_IN);

                LoanRequest loanRequest;

                loanRequest = JsonConvert.DeserializeObject<LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);
                Console.WriteLine();

                // The realy funny business happens here..
                // So how do we know which bank/routing key is witch? For now, just assume...
                // Should we remove the bank list here or not at all?
                //loanRequest.Banks = null;
                switch(routingKey)
                {
                    case "0":
                        handleWebServiceBank(loanRequest);
                        break;
                    case "1":
                        handleRabbitMQXMLBank(loanRequest);
                        break;
                    case "2":
                        handleRabbitMQJSONBank(loanRequest);
                        break;
                    case "3":
                        handleRabbitMQOurBank(loanRequest);
                        break;
                }
            }
            );
        }

        private static void handleWebServiceBank(LoanRequest loanRequest)
        {
            WebServiceBank.WebServiceBank webBank = new WebServiceBank.WebServiceBank();
            decimal msg = webBank.ProcessLoanRequest(loanRequest.SSN, loanRequest.CreditScore, loanRequest.Amount, loanRequest.Duration);
            Utility.HandleMessaging.SendMessage<decimal>("group1_bank_out", msg);
        }

        private static void handleRabbitMQXMLBank(LoanRequest loanRequest)
        {
            //TODO: Make cphbusiness.bankXML work
            DateTime dtDuraion = new DateTime(1970, 1, 1).AddMonths(loanRequest.Duration);
            string msg = string.Format("<LoanRequest><ssn>{0}</ssn><creditScore>{1}</creditScore><loanAmount>{2}</loanAmount><loanDuration>{3}</loanDuration></LoanRequest>", 
                loanRequest.SSN, 
                loanRequest.CreditScore, 
                loanRequest.Amount, 
                dtDuraion.ToString("yyyy-MM-dd HH:mm:ss:ff CET") // since we dont care about hours and so on, time zone info is useless
                );
            // Dunno the routing key..
            Utility.HandleMessaging.SendMessage("cphbusiness.bankXML", "", msg, "fanout");
            // And how do we get the message back?
        }


        private static void handleRabbitMQJSONBank(LoanRequest loanRequest)
        {
            //TODO: Make cphbusiness.bankJSON work
            string msg = string.Format("{\"ssn\":{0},\"creditScore\":{1},\"loanAmount\":{2},\"loanDuration\":{3}}", loanRequest.SSN, loanRequest.CreditScore, loanRequest.Amount, loanRequest.Duration);
            // Dunno the routing key..
            Utility.HandleMessaging.SendMessage("cphbusiness.bankJSON", "", msg, "fanout");
            // And how do we get the message back?
        }

        private static void handleRabbitMQOurBank(LoanRequest loanRequest)
        {
            //SSN;CreditScore;Amount;Duration
            string msg = string.Format("{0};{1};{2};{3}", loanRequest.SSN, loanRequest.CreditScore, loanRequest.Amount, loanRequest.Duration);
            Utility.HandleMessaging.SendMessage<string>(QUEUE_OUT, msg);
        }
    }
}
