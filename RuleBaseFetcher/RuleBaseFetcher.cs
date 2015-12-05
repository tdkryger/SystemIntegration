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


                localhost.RuleBase rb = new localhost.RuleBase();
                
                loanRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<LoanBroker.model.LoanRequest>(Encoding.UTF8.GetString(ea.Body));

                Console.WriteLine("<--Message content:");
                Console.WriteLine("<--" + loanRequest);


                //Something stupid going on here...
                // Problems with type casting between the webservice's version of LoanBroker.model and the Fetcher's version of LoanBroker.model
                localhost.Bank[] banks = rb.GetBanks(loanRequest.Amount, loanRequest.CreditScore, loanRequest.Duration, loanRequest.SSN);
                List<LoanBroker.model.Bank> realBanks = new List<LoanBroker.model.Bank>();
                foreach(localhost.Bank b in banks)
                {
                    realBanks.Add(new LoanBroker.model.Bank(b.Id, b.Name));
                }

                //Console.WriteLine("<--Enriched message content:");
                //Console.WriteLine("<--" + loanRequest);

                //TODO: Figure out how to pass the list of banks AND the loanRequest down the line....
                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                Utility.HandleMessaging.SendMessage<List<LoanBroker.model.Bank>>(QUEUE_OUT, realBanks);
                Utility.HandleMessaging.SendMessage<LoanBroker.model.LoanRequest>(QUEUE_OUT, loanRequest);
            });
        }
    }
}


