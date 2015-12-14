using System;

namespace SimpleBank
{
    /// <summary>
    /// A simple bank implementation
    /// </summary>
    public static class Bank
    {
        #region Public methods
        /// <summary>
        /// Process the loan request
        /// </summary>
        /// <param name="ssn">Social security number</param>
        /// <param name="creditScore">0 to 800</param>
        /// <param name="amount">The amount</param>
        /// <param name="duration">The duration</param>
        /// <returns>The interest rate</returns>
        public static decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration)
        {
            Random rnd = new Random();

            return (((decimal)rnd.Next(10, 5999)) / 100);
        }

        #endregion
    }
}