using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQConsumer
{
    public class RabbitMQTopicConsumer : IRabbitMQConsumer
    {
        private const string QUEUE_NAME = "WebAppQueueTopic";
        private const string EXCHANGE_NAME = "webappTopicExchange";
        private const string ROUTING_KEY = "topic.*"; // The routing key is used to specify the topic.

        public RabbitMQTopicConsumer()
        {

        }

        public void Consume(IModel channel)
        {
            Console.WriteLine("------------------ Topic consumer ------------------");
            // QueueDeclare creates a Queue in RabbitMQ
            channel.QueueDeclare(
                QUEUE_NAME,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // QueueBind creates a link between exchange and queue
            // Note: Previous messages sent to the queue prior to the binding are not going to be received by the consumer.
            channel.QueueBind(QUEUE_NAME, EXCHANGE_NAME, ROUTING_KEY);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var msg = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"Consumed message: {msg}");
            };

            channel.BasicConsume(QUEUE_NAME, true, consumer);
        }
    }
}
