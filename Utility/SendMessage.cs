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
        public static void SendMessage<T>(IModel channel, string queueName, T messageObject)
        {
            try
            {
                byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageObject));

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", messageObject.ToString());
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
