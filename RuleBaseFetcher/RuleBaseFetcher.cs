using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBaseFetcher
{
    class RuleBaseFetcher
    {
        private static string QUEUE_OUT = "group1_bankfetcher_out";
        private static string QUEUE_IN = "group1_creditbureau_out";

        static void Main(string[] args)
        {
            Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, RabbitMQ.Client.Events.BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanBroker.model.LoanRequest loanRequest;


                //RuleBaseInterface.RuleBaseSoapClient ruleBase = new RuleBaseInterface.RuleBaseSoapClient();
                //RuleBaseClient.RuleBaseSoapClient ruleBase = new RuleBaseClient.RuleBaseSoapClient();

                loanRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<LoanBroker.model.LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);


                //Something stupid going on here...
                // Problems with type casting between the webservice's version of LoanBroker.model and the Fetcher's version of LoanBroker.model
                // Look at http://www.codeproject.com/Articles/15967/How-to-Return-a-User-Defined-Object-from-Webservic
                //Bank[] banks = ruleBase.GetBanks(new RuleBaseInterface.LoanRequest()
                //{
                //    Amount = loanRequest.Amount,
                //    CreditScore = loanRequest.CreditScore,
                //    Duration = loanRequest.Duration,
                //    SSN = loanRequest.SSN
                //});
                // RuleBaseInterface.Bank[] tmpBanks = ruleBase.GetBanks(loanRequest.Amount, loanRequest.CreditScore, loanRequest.Duration, loanRequest.SSN);

                //loanRequest.CreditScore = service.creditScore(loanRequest.SSN);

                Console.WriteLine("<--Enriched message content:");
                Console.WriteLine("<--" + loanRequest);

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                // Utility.HandleMessaging.SendMessage<List<Bank>>(QUEUE_OUT, loanRequest);
            });
        }
    }
}


