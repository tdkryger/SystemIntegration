using LoanBroker.model;
using LoanBroker.Utility;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Aggregator
{
    public class Aggregator
    {
        private static long TIMEOUT = 500;
        private static int MIN_BANK_RESPONSES = 4;
        private static int TTL = 60000000;

        static readonly object _listLockObject = new object();

        private static readonly Timer _timeOut = new Timer();

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

        private static Dictionary<string, Response> _responeList = new Dictionary<string, Response>();


        public static void Main(string[] args)
        {
            _timeOut.Interval = TIMEOUT;
            _timeOut.Enabled = true;
            _timeOut.Elapsed += _timeOut_Elapsed;
            //TODO: A timer to handle timeouts. It could be 1 timer that runs through the list

            Console.Title = "Aggregator";
            Console.SetWindowSize(80, 5);
            Console.WriteLine("Listening on queue: " + Queues.NORMALIZER_OUT);

            HandleMessaging.RecieveMessage(Queues.NORMALIZER_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.NORMALIZER_OUT);

                LoanResponse loanResponse;

                loanResponse = JsonConvert.DeserializeObject<LoanResponse>(Encoding.UTF8.GetString(ea.Body));

                lock (_listLockObject)
                {
                    if (!_responeList.ContainsKey(loanResponse.SSN))
                    {
                        // New SSN
                        _responeList.Add(loanResponse.SSN,
                           new Response()
                           {
                               LoanResponses = new List<LoanResponse>()
                           }
                           );

                    }
                }

                if (_responeList.ContainsKey(loanResponse.SSN))
                {
                    lock (_listLockObject)
                    {
                        _responeList[loanResponse.SSN].LastResponseTime = DateTime.Now;
                        _responeList[loanResponse.SSN].LoanResponses.Add(loanResponse);
                    }
                    handleListStuff();


                }
                //else
                // Throw some exception or logit
            });
        }

        private static void handleListStuff()
        {
            Dictionary<string, Response> responeListCopy = new Dictionary<string, Response>();
            lock (_listLockObject)
            {
                foreach (string key in _responeList.Keys)
                {
                    responeListCopy.Add(key, _responeList[key]);
                }
            }
            foreach (string ssn in responeListCopy.Keys)
            {
                // "Har vi nok banker" eller "timeout" => Behandl og send
                if (responeListCopy[ssn].LoanResponses.Count >= MIN_BANK_RESPONSES || (responeListCopy[ssn].LastResponseTime < DateTime.Now.AddMilliseconds(TIMEOUT)))
                {
                    // Do some sweet aggregating!
                    //responeList[ssn].LoanResponses.Sort(new LoanResponseComparer());
                    int idx = int.MinValue;
                    decimal LowestOutPut = decimal.MaxValue;
                    for (int i = 0; i < responeListCopy[ssn].LoanResponses.Count; i++)
                    {
                        if (responeListCopy[ssn].LoanResponses[i].InterestRate < LowestOutPut)
                        {
                            LowestOutPut = responeListCopy[ssn].LoanResponses[i].InterestRate;
                            idx = i;
                        }
                    }

                    if (idx > int.MinValue)
                    {
                        Console.WriteLine("<--Sending message on queue: " + Queues.LOANBROKER_OUT);
                        Console.WriteLine();
                        // Need a TTL on this message....
                        HandleMessaging.SendMessage<LoanResponse>(
                            Queues.LOANBROKER_OUT, 
                            responeListCopy[ssn].LoanResponses[idx], 
                            TTL);
                        lock (_listLockObject)
                        {
                            _responeList.Remove(ssn);
                        }
                    }
                }
            }

        }

        private static void _timeOut_Elapsed(object sender, ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}