using RabbitMQ.Client;

namespace RabbitMQExamples
{
    public class RabbitMQDirect : IRabbitMQExchange
    {
        private IModel _channel;
        public RabbitMQDirect(IModel channel)
        {
            _channel = channel;
        }
        public void PublishMessage(string exchangeName, string message, string? routingKey)
        {
            Console.WriteLine("------------------ Direct publisher ------------------");
            // ExchangeDeclare will create an exchange in RabbitMQ
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, true);

            // Since amqp is a binary protocol then we need to convert the message into bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Publish message to RabbitMQ
            // routingKey is the queue to which this message is going to be routed
            _channel.BasicPublish(exchangeName, routingKey, null, bytes);
            Console.WriteLine($"Published Message: {message}");

        }
    }
}
