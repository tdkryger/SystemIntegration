using System;

namespace LoanBroker.Utility
{
    public class BankingUtility
    {
        public static decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration)
        {
            Random rnd = new Random();

            return (((decimal)rnd.Next(10, 5999)) / 100);
        }
    }
}