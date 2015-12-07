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
        private static IModel _channel = null;

        /// <summary>
        /// Gets the Channel (Singleton)
        /// </summary>
        private static IModel Channel
        {
            get
            {
                return _channel == null ? new ConnectionFactory() { HostName = "datdb.cphbusiness.dk" }.CreateConnection().CreateModel() : _channel;
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
                Console.WriteLine(" No message has been sent. ");

                result = false;
            }

            // Test for fetching .resx resources (Det er måske bare forvirrende med brugen af resx filer her?)
            //// Console.WriteLine("Queue name: " + messaging_resources.ResourceManager.GetString("QueueNameNormalizerOut"));

            return result;
        }

        /// <summary>
        /// Sends a message with the given type as the body, in the given queue
        /// </summary>
        /// <typeparam name="T">Type of object to send</typeparam>
        /// <param name="exchangeName">The name of the exchange</param>
        /// <param name="exchangeType">Type of exchange. Defaults to direct</param>
        /// <param name="messageObject">The object to send</param>
        /// <param name="routingKey">the routing key</param>
        /// <returns></returns>
        public static bool SendMessage<T>(string exchangeName, string routingKey, T messageObject, string exchangeType = "direct")
        {
            bool result = true; ;

            try
            {
                Channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

                string jSonString = JsonConvert.SerializeObject(messageObject);
                byte[] body = Encoding.UTF8.GetBytes(jSonString);

                Channel.BasicPublish(exchange: exchangeName,
                                     routingKey: routingKey, 
                                     basicProperties: null, 
                                     body: body);
            }
            catch (Exception e)
            {
                Console.WriteLine(" An error occured: ");
                Console.WriteLine(e.Message);
                Console.WriteLine(" No message has been sent. ");

                result = false;
            }
            return result;
        }

        /// <summary>
        /// Sends a message with the given type as the body, in the given queue
        /// </summary>
        /// <param name="exchangeName">The name of the exchange</param>
        /// <param name="exchangeType">Type of exchange. Defaults to direct</param>
        /// <param name="messageString">The message to send</param>
        /// <param name="routingKey">the routing key</param>
        /// <returns></returns>
        public static bool SendMessage(string exchangeName, string routingKey, string messageString, string exchangeType = "direct")
        {
            bool result = true; ;

            try
            {
                Channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

                byte[] body = Encoding.UTF8.GetBytes(messageString);

                Channel.BasicPublish(exchange: exchangeName, 
                                     routingKey: routingKey, 
                                     basicProperties: null, 
                                     body: body);
            }
            catch (Exception e)
            {
                Console.WriteLine(" An error occured: ");
                Console.WriteLine(e.Message);
                Console.WriteLine(" No message has been sent. ");
                result = false;
            }
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

        /// <summary>
        /// Hooks the "method" method to "consumer.Recieve" eventhandler
        /// </summary>
        /// <param name="exchangeName">The name of the exchange</param>
        /// <param name="routingKey">the routing key</param>
        /// <param name="method">The method to call when the declared queue recieves a message</param>
        /// /// <param name="exchangeType">Type of exchange. Defaults to direct</param>
        public static EventingBasicConsumer RecieveMessage(string exchangeName, string routingKey, EventHandler<BasicDeliverEventArgs> method, string exchangeType = "direct")
        {
            EventingBasicConsumer consumer;
            
            Channel.ExchangeDeclare(exchange: exchangeName, type: exchangeType);

            string queueName = Channel.QueueDeclare().QueueName;

            Channel.QueueDeclare(queue: queueName, 
                                 durable: false, 
                                 exclusive: false, 
                                 autoDelete: false, 
                                 arguments: null);
            Channel.QueueBind(queue: queueName, 
                              exchange: exchangeName, 
                              routingKey: routingKey);

            consumer = new EventingBasicConsumer(Channel);
            consumer.Received += method;

            Channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);

            return consumer;
        }
    }
}