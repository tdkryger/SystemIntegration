using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aggregator
{
    public class Program
    {
        private static long TIMEOUT = 6500;
        private static int MIN_BANK_RESPONSES = 4;

        private class Response
        {
            public DateTime LastResponseTime { get; set; }
            public List<LoanResponse> LoanResponses { get; set; }
        }

        private class LoanResponseComparer : IComparer<LoanResponse>
        {
            public int Compare(LoanResponse x, LoanResponse y)
            {
                return x.InterestRate.CompareTo(y.InterestRate);
            }
        }

        private static Dictionary<string, Response> responeList = new Dictionary<string, Response>();


        public static void Main(string[] args)
        {
            HandleMessaging.RecieveMessage(Queues.NORMALIZER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.NORMALIZER_OUT);

                LoanResponse loanResponse;

                loanResponse = JsonConvert.DeserializeObject<LoanResponse>(Encoding.UTF8.GetString(ea.Body));

                if (!responeList.ContainsKey(loanResponse.SSN))
                {
                    // New SSN
                    responeList.Add(loanResponse.SSN,
                       new Response()
                       {
                           LoanResponses = new List<LoanResponse>()
                       }
                       );

                }
                if (responeList.ContainsKey(loanResponse.SSN))
                {
                    responeList[loanResponse.SSN].LastResponseTime = DateTime.Now;
                    responeList[loanResponse.SSN].LoanResponses.Add(loanResponse);

                    foreach (string ssn in responeList.Keys)
                    {

                        // "Har vi nok banker" eller "timeout" => Behandl og send
                        if (responeList[ssn].LoanResponses.Count >= MIN_BANK_RESPONSES || (responeList[ssn].LastResponseTime < DateTime.Now.AddMilliseconds(TIMEOUT)))
                        {
                            // Do some sweet aggregating!
                            //responeList[ssn].LoanResponses.Sort(new LoanResponseComparer());
                            int idx =int.MinValue;
                            decimal LowestOutPut = decimal.MaxValue;
                            for (int i = 0; i < responeList[ssn].LoanResponses.Count; i++)
                            {
                                if (responeList[ssn].LoanResponses[i].InterestRate < LowestOutPut)
                                {
                                    LowestOutPut = responeList[ssn].LoanResponses[i].InterestRate;
                                    idx = i;
                                }
                            }

                            if (idx > int.MinValue)
                            {
                                Console.WriteLine("<--Sending message on queue: " + Queues.AGGREGATOR_OUT);
                                Console.WriteLine();
                                HandleMessaging.SendMessage<LoanResponse>(Queues.AGGREGATOR_OUT, responeList[ssn].LoanResponses[idx]);
                            }
                        }
                    }
                }
                //else
                // Throw some exception or logit
            });
        }

    }
}