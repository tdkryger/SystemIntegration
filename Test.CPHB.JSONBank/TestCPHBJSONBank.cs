using LoanBroker.Utility;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CPHB.JSONBank
{
    class TestCPHBJSONBank
    {
        static void Main(string[] args)
        {
            string routingKey = LoanBroker.Utility.BankingUtility.ROUTING_KEY_RabbitMQJSONBank;


            Console.Title = "RabbitMQJSONBankNormalizer";
            //Console.SetWindowSize(80, 5);
            Console.WriteLine("Listening on queue: " + Queues.RABBITMQJSONBANK_OUT);

            HandleMessaging.RecieveMessage(Queues.RABBITMQJSONBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQJSONBANK_OUT);

                string msgIn = Encoding.UTF8.GetString(ea.Body);

                Console.WriteLine("Message is: " + msgIn);

                Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                Console.WriteLine();
            });

            /*
            String format from pdf:
            string msg = "{\"ssn\":" + loanRequest.SSN + ",\"creditScore\":" + loanRequest.CreditScore.ToString() +  
                ",\"loanAmount\":" + loanRequest.Amount.ToString() + ",\"loanDuration\":" + loanRequest.Duration +" }";

            but getting this from the bank:

            Exception: Something went wrong.Data should be sent like: { "ssn":1605789787,"loanAmount":10.0,"loanDuration":360,"rki":false}
            Can not instantiate value of type[simple type, class dk.cphbusiness.si.banktemplate.JsonDTO.BankLoanDTO] from JSON String; no single-String constructor/factory method

            */
            //string msg = "{ \"ssn\":1605789787,\"loanAmount\":10.0,\"loanDuration\":360,\"rki\":false}";
            string msg = "{ \"ssn\":1605789787,\"creditScore\":598,\"loanAmount\":10.0,\"loanDuration\":360}";
            //string msg = "{ \"ssn\":" + loanRequest.SSN
            //    + ",\"loanAmount\":" + loanRequest.Amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"))
            //    + ",\"loanDuration\":" + loanRequest.Duration.ToString()
            //    + ",\"rki\":false }";
            //string msg = "{ \"ssn\":" + loanRequest.SSN.Replace("-", "")
            //    + ",\"creditScore\":"  + loanRequest.CreditScore.ToString()
            //    + ",\"loanAmount\":" + loanRequest.Amount.ToString(CultureInfo.CreateSpecificCulture("en-GB")) 
            //    + ",\"loanDuration\":" + loanRequest.Duration + " }";

            Console.WriteLine("--> Sending " + msg + " to cphbusiness.bankJSON");
            HandleMessaging.SendMessage("cphbusiness.bankJSON", routingKey, Queues.RABBITMQJSONBANK_OUT, msg, "fanout");
        }
    }
}
