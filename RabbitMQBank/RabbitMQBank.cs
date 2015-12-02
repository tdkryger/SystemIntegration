﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQBank
{
    class RabbitMQBank
    {
        //static IModel SendChannel;
        static IModel ReceiveChannel;
        static IConnection connection;

        static string SendQueueName = "OurSendBankQueue";
        static string ReceiveQueueName = "OurRecieveBankQueue";

        static void Main(string[] args)
        {
            /*
                1)  Factory
                2)  Wait for message
                3)  Responde to message
                4)  Goto 2
            */

            var factory = new ConnectionFactory()
            {
                HostName = "datdb.cphbusiness.dk"
            };

            using (connection = factory.CreateConnection())
            {
                using (ReceiveChannel = connection.CreateModel())
                {

                    ReceiveChannel.QueueDeclare(queue: ReceiveQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(ReceiveChannel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);

                        SendReply(connection, message);
                    };

                    ReceiveChannel.BasicConsume(queue: ReceiveQueueName, noAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        static void SendReply(IConnection connection, string theMessage)
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: SendQueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //string message = "Hello Rabbit!";
                //Split recieveMessage into params
                // ssn;creditScore;amount;duration
                string[] parts = theMessage.Split(';');
                string ssn = parts[0];
                int creditScore = 0;
                int.TryParse(parts[1], out creditScore);
                double amount = 0;
                double.TryParse(parts[2], out amount);
                int duration = 0;
                int.TryParse(parts[3], out duration);

                double sendMessage = SimpleBank.Bank.ProcessLoanRequest(ssn, creditScore, amount, duration);

                var body = Encoding.UTF8.GetBytes(sendMessage.ToString());

                channel.BasicPublish(exchange: "",
                                     routingKey: SendQueueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", sendMessage.ToString());
            }
        }
    }
}