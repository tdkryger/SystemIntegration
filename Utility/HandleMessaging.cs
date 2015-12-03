using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class HandleMessaging
    {
        private static IModel channel = null;

        private static IModel Channel
        {
            get
            {
                return channel == null ? new ConnectionFactory() { HostName = "datdb.cphbusiness.dk" }.CreateConnection().CreateModel() : channel;
            }
        }

        public static void SendMessage<T>(string queueName, T messageObject)
        {
            try
            {
                string jSonString = JsonConvert.SerializeObject(messageObject);
                byte[] body = Encoding.UTF8.GetBytes(jSonString);

                Channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", jSonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(" An error occured: ");
                Console.WriteLine(e.Message);
                Console.WriteLine(" No message has ben sent. ");
            }
        }

        public static void RecieveMessage()
        {

        }
    }
}
