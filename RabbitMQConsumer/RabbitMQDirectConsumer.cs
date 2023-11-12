using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace RabbitMQConsumer
{
    public class RabbitMQDirectConsumer : IRabbitMQConsumer
    {
        private const string QUEUE_NAME = "WebAppQueueDirect";
        private const string EXCHANGE_NAME = "webappDirectExchange";
        private const string ROUTING_KEY = "key.direct"; // For direct exchange type, the routing key is the parameter used to route messages to corresponding queues.
        public RabbitMQDirectConsumer()
        {

        }

        public void Consume(IModel channel)
        {
            Console.WriteLine("------------------ Direct consumer ------------------");
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
