using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class RabbitMQHeadersConsumer : IRabbitMQConsumer
    {
        private const string QUEUE_NAME = "WebAppQueueHeaders";
        private const string EXCHANGE_NAME = "webappHeadersExchange";
        private const string ROUTING_KEY = ""; // For headers exchange type, the routing key is not needed.

        public RabbitMQHeadersConsumer()
        {
            
        }

        public void Consume(IModel channel)
        {
            Console.WriteLine("------------------ Headers consumer ------------------");
            // QueueDeclare creates a Queue in RabbitMQ
            channel.QueueDeclare(
                QUEUE_NAME, 
                durable: true, 
                exclusive: false, 
                autoDelete: false,
                arguments: null);

            // Headers needed as dictionary
            var header = new Dictionary<string, object> { { "department", "dev" } };

            // QueueBind creates a link between exchange and queue
            // Note: Previous messages sent to the queue prior to the binding are not going to be received by the consumer.
            channel.QueueBind(QUEUE_NAME, EXCHANGE_NAME, ROUTING_KEY, arguments: header);

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
