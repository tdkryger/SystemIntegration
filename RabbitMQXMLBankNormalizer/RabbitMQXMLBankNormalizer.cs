using LoanBroker.model;
using LoanBroker.Utility;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;

namespace RabbitMQXMLBankNormalizer
{
    public class RabbitMQXMLBankNormalizer
    {
        public static void Main(string[] args)
        {
            Console.Title = "RabbitMQXMLBankNormalizer";
            Console.SetWindowSize(80, 5);
            HandleMessaging.RecieveMessage(Queues.RABBITMQXMLBANK_OUT, (object model, BasicDeliverEventArgs ea) =>
            {
                Console.WriteLine("<--Message recieved on queue: " + Queues.RABBITMQXMLBANK_OUT);
                try
                {
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
                            BankName = ea.RoutingKey.Split('_')[1] // Gets the bank name from the queue name
                        };
                    }

                    Console.WriteLine("<--Sending message on queue: " + Queues.NORMALIZER_OUT);
                    Console.WriteLine();
                    HandleMessaging.SendMessage<LoanResponse>(Queues.NORMALIZER_OUT, loanResponse);
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            });
        }

        public class XMLBankResponse
        {
            public string SSN { get; set; }
            public decimal InterestRate { get; set; }
        }
    }
}