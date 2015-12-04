using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanBroker.model
{
    /*
        I forhold til System Integrations projektet fik jeg et par ideer til at håndtere nogle af detaljerne omkring det at vores Rule Base Web Service (RBWS) skal kende de banker Loan Broker'en kan vælge imellem, og hvordan vi får flyttet listen af banker fra RuleBaseFetcher (RBF):
            * Når vi starter en UniversalTranslater op, så giver vi den en parameter, så den ved hvilken bank, af de 4, den skal snakke med. Den sender så en [Bank] på en (ny) queue (bank_announce_queue), som vores RBF lytter på. Denne [Bank] bliver en del af en liste RBF vedligeholder, og som bliver sendt RBWS som parameter.
            * Hvis vi kommer i den situation hvor RBF bliver startet op efter UniversalTranslater (genstart or something), bliver vi nød til at have endnu en queue som RBF sender beskeder til UniversalTranslater's om at de skal sende en besked på bank_announce_queue. UniversalTranslater skal så subscribe til denne queue. Alternativt skal UniversalTranslater'ne periodisk sende en [Bank] på den anden queue. 
            * For at få valget/listen af valgte banker fra RBF til UniversalTranslater, foreslår jeg at vores [LoanRequest] får List<Bank> som parameter, som RBF udfylder
    */


    /// <summary>
    /// Simple Bank Class
    /// </summary>
    public class Bank
    {
        #region Properties
        /// <summary>
        /// ID of bank. We only have 4 banks, so valid values are 1 to 4
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// A descriptive name for the bank
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="id">ID of the bank</param>
        /// <param name="name"></param>
        public Bank(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        #endregion
    }
}
