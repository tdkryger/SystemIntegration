using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleBaseFetcher
{
    public class Program
    {
        private static string QUEUE_IN = "";
        private static string QUEUE_OUT = "";

        public static void Main(string[] args)
        {
            Utility.HandleMessaging.RecieveMessage(QUEUE_IN, (object model, BasicDeliverEventArgs ea) =>
            {

            });
        }
    }
}