﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBank
{
    /// <summary>
    /// A simple bank implementation
    /// </summary>
    public static class Bank
    {
        //SDASD
        #region Public methods
        /// <summary>
        /// Process the loan request
        /// </summary>
        /// <param name="ssn">Social security number</param>
        /// <param name="creditScore">0 to 800</param>
        /// <param name="amount">The amount</param>
        /// <param name="duration">The duration</param>
        /// <returns>The interest rate</returns>
        public static double ProcessLoanRequest(string ssn, int creditScore, double amount, int duration)
        {
            Random rnd = new Random();

            return (((double)rnd.Next(10, 5999)) / 100);
        }

        #endregion
    }
}