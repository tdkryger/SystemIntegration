using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBroker
{
    interface TranslatorBase
    {
        #region Public methods
        void SendMessage(string routingKey, string exchangeName, model.LoanRequest loanRequest);
        #endregion
    }
}
