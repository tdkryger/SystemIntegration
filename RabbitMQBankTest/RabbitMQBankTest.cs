using LoanBroker.model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitMQBankTest
{
    class RabbitMQBankTest
    {
        static IModel SendChannel;
        //static IModel ReceiveChannel;
        static IConnection connection;

        static string SendQueueName = "group1_delegater_out";
        static string ReceiveQueueName = "group1_bank_out";

        static void Main(string[] args)
        {


            var factory = new ConnectionFactory()
            {
                HostName = "datdb.cphbusiness.dk"
            };

            using (connection = factory.CreateConnection())
            {
                using (SendChannel = connection.CreateModel())
                {
                    Utility.HandleMessaging.SendMessage<LoanRequest>(SendQueueName, new LoanRequest() { Amount = 123, CreditScore = 12, Duration = 12, SSN = "785637-1234" });

                    //SendChannel.QueueDeclare(queue: SendQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    //string sendMessage = "123456-7890;300;1208.60;17";
                    //var sendBody = Encoding.UTF8.GetBytes(sendMessage.ToString());

                    //SendChannel.BasicPublish(exchange: "", routingKey: SendQueueName, basicProperties: null, body: sendBody);
                    //Console.WriteLine(" [x] Sent {0}", sendMessage.ToString());

                    ReceiveReply(connection);
                }
            }

        }

        static void ReceiveReply(IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: ReceiveQueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: ReceiveQueueName,
                                     noAck: true,
                                     consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
