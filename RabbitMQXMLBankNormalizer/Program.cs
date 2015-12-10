using LoanBroker.model;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace RabbitMQXMLBankNormalizer
{
    public class Program
    {
        private static string QUEUE_IN = "group1_rabbitmqxmlbank_out";
        private static string QUEUE_OUT = "group1_normalizer_out";

        public static void Main(string[] args)
        {
            LoanBroker.Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + QUEUE_IN);

                LoanResponse loanResponse;
                XMLBankResponse bankResponse;
                System.Xml.Serialization.XmlSerializer serializer;

                serializer = new System.Xml.Serialization.XmlSerializer(typeof(XMLBankResponse));

                using (TextReader reader = new StringReader(Encoding.UTF8.GetString(ea.Body)))
                {
                    bankResponse = serializer.Deserialize(reader) as XMLBankResponse;

                    loanResponse = new LoanResponse()
                    {
                        InterestRate = bankResponse.InterestRate,
                        SSN = bankResponse.SSN,
                        BankName = ea.RoutingKey
                    };
                }

                Console.WriteLine("<--Sending message on queue: " + QUEUE_OUT);
                Console.WriteLine();
                LoanBroker.Utility.HandleMessaging.SendMessage<LoanResponse>(QUEUE_OUT, loanResponse);
            });
        }

        private class XMLBankResponse
        {
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}