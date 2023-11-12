using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class RabbitMQFanoutConsumer : IRabbitMQConsumer
    {
        private const string QUEUE_NAME = "WebAppQueueFanout";
        private const string EXCHANGE_NAME = "webappFanoutExchange";
        private const string ROUTING_KEY = ""; // For fanout exchange type, the routing key is not needed.
        public RabbitMQFanoutConsumer() { }

        public void Consume(IModel channel)
        {
            Console.WriteLine("------------------ Fanout consumer ------------------");
            // QueueDeclare creates a Queue in RabbitMQ
            channel.QueueDeclare(
                QUEUE_NAME, 
                durable: true, 
                exclusive: false, 
                autoDelete: false,
                arguments: null);

            // QueueBind creates a link between exchange and queue
            // Note: previous messages sent to the queue prior to the binding are not going to be received by the consumer.
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
