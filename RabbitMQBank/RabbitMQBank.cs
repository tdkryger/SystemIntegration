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
            Console.WriteLine("<--Listening for messages on queue: " + Queues.DELEGATER_OUT);
            HandleMessaging.RecieveMessage(Queues.DELEGATER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.DELEGATER_OUT);

                byte[] inBody = ea.Body;
                string message = Encoding.UTF8.GetString(inBody);

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + message);

                //SSN;CreditScore;Amount;Duration
                string[] parts = message.Split(';');
                string ssn = parts[0];
                int creditScore = 0;
                int.TryParse(parts[1], out creditScore);
                decimal amount = 0;
                decimal.TryParse(parts[2], out amount);
                int duration = 0;
                int.TryParse(parts[3], out duration);

                LoanBroker.model.OurBankResponse bankResponse = new LoanBroker.model.OurBankResponse()
                {
                    InterestRate = BankingUtility.ProcessLoanRequest(ssn, creditScore, amount, duration),
                    Name = "Our RabbitMQ Bank",
                    SSN = ssn
                };

                //decimal sendMessage = BankingUtility.ProcessLoanRequest(ssn, creditScore, amount, duration);

                string msg = JsonConvert.SerializeObject(bankResponse);

                Console.WriteLine("<--Sending message on queue: " + Queues.BANK_OUT + " > " + msg);
                Console.WriteLine();

                HandleMessaging.SendMessage<LoanBroker.model.OurBankResponse>(Queues.BANK_OUT, bankResponse);
            });
        }
    }
}