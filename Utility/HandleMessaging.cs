using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Utility
{
    /// <summary>
    /// A static class that handles all messaging issues
    /// </summary>
    public static class HandleMessaging
    {
        private static IModel channel = null;

        /// <summary>
        /// Gets the Channel (Singleton)
        /// </summary>
        private static IModel Channel
        {
            get
            {
                return channel == null ? new ConnectionFactory() { HostName = "datdb.cphbusiness.dk" }.CreateConnection().CreateModel() : channel;
            }
        }

        /// <summary>
        /// Sends a message with the given type as the body, in the given queue
        /// </summary>
        /// <typeparam name="T">Type of object to send</typeparam>
        /// <param name="queueName">The queue name to send the message to</param>
        /// <param name="messageObject">The object to send</param>
        /// <returns></returns>
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

        /// <summary>
        /// Hooks the "method" method to "consumer.Recieve" eventhandler
        /// </summary>
        /// <param name="queueName">The queue name to recieve messages from</param>
        /// <param name="method">The method to call when the declared queue recieves a message</param>
        public static EventingBasicConsumer RecieveMessage(string queueName, EventHandler<BasicDeliverEventArgs> method)
        {
            EventingBasicConsumer consumer;

            Channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            consumer = new EventingBasicConsumer(Channel);
            consumer.Received += method;

            Channel.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);

            return consumer;
        }
    }
}