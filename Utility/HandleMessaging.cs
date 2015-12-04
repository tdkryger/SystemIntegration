using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Utility.Properties;
using RabbitMQ.Client.Events;

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

        public static bool SendMessage<T>(string queueName, T messageObject)
        {
            bool result = true; ;

            try
            {
                string jSonString = JsonConvert.SerializeObject(messageObject);
                byte[] body = Encoding.UTF8.GetBytes(jSonString);

                Channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
            catch (Exception e)
            {
                Console.WriteLine(" An error occured: ");
                Console.WriteLine(e.Message);
                Console.WriteLine(" No message has ben sent. ");
                result = false;
            }

            // Test for fetching .resx resources (Det er måske bare forvirrende med brugen af resx filer her?)
            //// Console.WriteLine("Queue name: " + messaging_resources.ResourceManager.GetString("QueueNameNormalizerOut"));

            return result;
        }

        public static void RecieveMessage(string queueName, Action<BasicDeliverEventArgs> method)
        {
            Channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(Channel);

            consumer.Received += (model, ea) =>
            {
                method(ea);
            };

            Channel.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);
        }
    }
}