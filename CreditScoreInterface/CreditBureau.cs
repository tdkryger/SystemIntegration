using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreditScoreInterface
{
    /// <summary>
    /// blablabla
    /// </summary>
    public static class CreditBureau
    {
        /// <summary>
        /// aekgjladkfjglkadhfgl
        /// </summary>
        /// <param name="ssn"></param>
        /// <returns></returns>
        public static int GetCreditScore(string ssn)
        {
           
            var csi = new CreditScoreService.CreditScoreServiceClient();
            int returnValue = csi.creditScore(ssn);
            csi = null;
            return returnValue;
        }
    }
}
