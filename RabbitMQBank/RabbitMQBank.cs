using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Resources;

namespace RabbitMQBank
{
    class RabbitMQBank
    {
        static IModel ReceiveChannel;
        static IConnection connection;

        static string SendQueueName = "group1_bank_out";
        static string ReceiveQueueName = "group1_delegater_out";

        static void Main(string[] args)
        {
            /*
            group1_delegater_out
                1)  Factory
                2)  Wait for message
                3)  Responde to message
                4)  Goto 2
            */

            // Get Queue names from LoanBroker Project
            //ResourceManager resMan = new ResourceManager("LoanBroker", System.Reflection.Assembly.GetExecutingAssembly());
            //ReceiveQueueName = resMan.GetString("QueueNameBankDelegator");
            //SendQueueName = resMan.GetString("QueueNameBankOut");

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

                    consumer.Received += Consumer_Received;

                    ReceiveChannel.BasicConsume(queue: ReceiveQueueName, noAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            byte[] inBody = e.Body;
            string message = Encoding.UTF8.GetString(inBody);
            Console.WriteLine(" [x] Received {0}", message);

            //using (var channel = connection.CreateModel())
            //{


                //channel.QueueDeclare(queue: SendQueueName,
                //                     durable: false,
                //                     exclusive: false,
                //                     autoDelete: false,
                //                     arguments: null);

                ////string message = "Hello Rabbit!";
                ////Split recieveMessage into params
                //// ssn;creditScore;amount;duration
                string[] parts = message.Split(';');
                string ssn = parts[0];
                int creditScore = 0;
                int.TryParse(parts[1], out creditScore);
                double amount = 0;
                double.TryParse(parts[2], out amount);
                int duration = 0;
                int.TryParse(parts[3], out duration);

                double sendMessage = SimpleBank.Bank.ProcessLoanRequest(ssn, creditScore, amount, duration);

                Utility.HandleMessaging.SendMessage<double>(SendQueueName, sendMessage);

                //byte[] outBody = Encoding.UTF8.GetBytes(sendMessage.ToString());

                //channel.BasicPublish(exchange: "",
                //                     routingKey: SendQueueName,
                //                     basicProperties: null,
                //                     body: outBody);
                //Console.WriteLine(" [x] Sent {0}", sendMessage.ToString());
            //}
        }
    }
}
