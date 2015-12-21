using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQBank
{
    public class RabbitMQBank
    {
        static void Main(string[] args)
        {
            Console.Title = "RabbitMQBank";
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(80, 5);
            Console.WriteLine("<--Listening for messages on queue: " + Queues.RABBITMQOURBANK_IN);
            HandleMessaging.RecieveMessage(Queues.RABBITMQOURBANK_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQOURBANK_IN);

                byte[] inBody = ea.Body;
                string message = Encoding.UTF8.GetString(inBody);

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + message);

                //SSN;CreditScore;Amount;Duration
                message = message.Replace("\"", "");
                string[] parts = message.Split(';');
                string ssn = parts[0];
                int creditScore = 0;
                int.TryParse(parts[1], out creditScore);
                decimal amount = 0;
                decimal.TryParse(parts[2], out amount);
                int duration = 0;
                int.TryParse(parts[3], out duration);

                LoanBroker.model.LoanResponse loanResponse = new LoanBroker.model.LoanResponse()
                {
                    InterestRate = BankingUtility.ProcessLoanRequest(ssn, creditScore, amount, duration),
                    BankName = "Our RabbitMQ Bank",
                    SSN = ssn
                };

                //decimal sendMessage = BankingUtility.ProcessLoanRequest(ssn, creditScore, amount, duration);

                string msg = JsonConvert.SerializeObject(loanResponse);

                Console.WriteLine("<--Sending message on queue: " + Queues.RABBITMQOURBANK_OUT + " > " + msg);
                Console.WriteLine();

                HandleMessaging.SendMessage<LoanBroker.model.LoanResponse>(Queues.RABBITMQOURBANK_OUT, loanResponse);
            });
        }
    }
}