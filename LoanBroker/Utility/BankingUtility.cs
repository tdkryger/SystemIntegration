using System;

namespace LoanBroker.Utility
{
    public class BankingUtility
    {
        public readonly static string ROUTING_KEY_RabbitMQJSONBank = "RabbitMQJSONBank";
        public readonly static string ROUTING_KEY_RabbitMQOURBank = "RabbitMQOURBank";
        public readonly static string ROUTING_KEY_RabbitMQXMLBank = "RabbitMQXMLBank";
        public readonly static string ROUTING_KEY_WebServiceBank = "WebServiceBank";

        public static decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration)
        {
            Random rnd = new Random();

            return (((decimal)rnd.Next(10, 5999)) / 100);
        }
    }
}