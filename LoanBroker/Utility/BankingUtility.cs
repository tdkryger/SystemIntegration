using System;

namespace LoanBroker.Utility
{
    public class BankingUtility
    {
        public readonly static string ROUTING_KEY_PREFIX = "RK_";
        public readonly static string ROUTING_KEY_RabbitMQJSONBank = string.Format("{0}RabbitMQJSONBank", ROUTING_KEY_PREFIX);
        public readonly static string ROUTING_KEY_RabbitMQOURBank = string.Format("{0}RabbitMQOURBank", ROUTING_KEY_PREFIX);
        public readonly static string ROUTING_KEY_RabbitMQXMLBank = string.Format("{0}RabbitMQXMLBank", ROUTING_KEY_PREFIX);
        public readonly static string ROUTING_KEY_WebServiceBank = string.Format("{0}WebServiceBank", ROUTING_KEY_PREFIX);

        public static decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration)
        {
            Random rnd = new Random();

            return (((decimal)rnd.Next(10, 5999)) / 100);
        }
    }
}