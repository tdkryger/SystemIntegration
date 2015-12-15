namespace LoanBroker.Utility
{
    public static class Queues
    {
        private const string PREFIX = "group1_";

        public readonly static string RABBITMQ_HOSTNAME = "datdb.cphbusiness.dk";

        public readonly static string NORMALIZER_OUT = string.Format("{0}normalizer_out", PREFIX);
        public readonly static string AGGREGATOR_OUT = string.Format("{0}aggregator_out", PREFIX);
        public readonly static string LOANBROKER_IN = string.Format("{0}loanbroker_in", PREFIX);
        public readonly static string LOANBROKER_OUT = string.Format("{0}loanbroker_out", PREFIX);
        public readonly static string CREDITBUREAU_OUT = string.Format("{0}creditbureau_out", PREFIX);
        public readonly static string BANK_OUT = string.Format("{0}bank_out", PREFIX);
        public readonly static string DELEGATER_OUT = string.Format("{0}delegater_out", PREFIX);
        public readonly static string RABBITMQJSONBANK_OUT = string.Format("{0}rabbitmqjsonbank_out", PREFIX);
        public readonly static string RABBITMQOURBANK_OUT = string.Format("{0}rabbitmqourbank_out", PREFIX);
        public readonly static string RABBITMQXMLBANK_OUT = string.Format("{0}rabbitmqxmlbank_out", PREFIX);
        public readonly static string BANKFETCHER_OUT = string.Format("{0}bankfetcher_out", PREFIX);
        public readonly static string RULEBASEFETCHER_OUT = string.Format("{0}rulebasefetcher_out", PREFIX);
        public readonly static string WEBSERVICEBANK_OUT = string.Format("{0}webservicebank_out", PREFIX);
    }
}