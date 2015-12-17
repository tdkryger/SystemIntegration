using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQJSONBankNormalizer
{
    public class RabbitMQJSONBankNormalizer
    {
        public static void Main(string[] args)
        {
            Console.Title = "RabbitMQJSONBankNormalizer";
            Console.SetWindowSize(80, 5);
            Console.WriteLine("Listening on queue: " + Queues.RABBITMQJSONBANK_OUT);

            HandleMessaging.RecieveMessage(Queues.RABBITMQJSONBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQJSONBANK_OUT);

                LoanResponse loanResponse;
                JsonBankResponse bankResponse;

                string msg = Encoding.UTF8.GetString(ea.Body);

                try
                {
                    bankResponse = JsonConvert.DeserializeObject<JsonBankResponse>(msg);
                    loanResponse = new LoanResponse()
                    {
                        InterestRate = bankResponse.InterestRate,
                        SSN = bankResponse.SSN,
                        BankName = ea.RoutingKey.Split('_')[1] // Gets the bank name from the queue name
                    };
                
                Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                Console.WriteLine();
                HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.BackgroundColor = ConsoleColor.Black;
                /*
                   String format from pdf:
                   string msg = "{\"ssn\":" + loanRequest.SSN + ",\"creditScore\":" + loanRequest.CreditScore.ToString() +  
                       ",\"loanAmount\":" + loanRequest.Amount.ToString() + ",\"loanDuration\":" + loanRequest.Duration +" }";

                   but getting this from the bank:

                   Exception: Something went wrong.Data should be sent like: { "ssn":1605789787,"loanAmount":10.0,"loanDuration":360,"rki":false}
                   Can not instantiate value of type[simple type, class dk.cphbusiness.si.banktemplate.JsonDTO.BankLoanDTO] from JSON String; no single-String constructor/factory method

                    Gonna send the pdf version, and just catch the exception here...
                */
            }
            });
        }

        private class JsonBankResponse
        {
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}