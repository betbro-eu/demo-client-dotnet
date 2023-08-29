using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

namespace Demo
{
    class Program
    {
        private const string CONNECTION_URI = "amqp://demo:demo@116.202.19.203:5672/demo";
        private const string EXCHANGE = "customer-exchange";
        private const string QUEUE = "customer-queue";
        private const string ROUTING_KEY = "#";

        static async Task Main(string[] args)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(CONNECTION_URI) };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(EXCHANGE, ExchangeType.Topic, durable: true);
            channel.QueueDeclare(QUEUE, exclusive: false, autoDelete: true);
            channel.QueueBind(QUEUE, EXCHANGE, ROUTING_KEY);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Console.WriteLine(message);
                dynamic obj = JsonConvert.DeserializeObject(message);
                Console.WriteLine(obj);

                await Task.Yield();
            };
            channel.BasicConsume(QUEUE, true, consumer);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}