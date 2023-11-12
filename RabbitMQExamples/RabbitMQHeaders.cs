using RabbitMQ.Client;

namespace RabbitMQExamples
{
    public class RabbitMQHeaders : IRabbitMQExchange
    {
        private IModel _channel;

        public RabbitMQHeaders(IModel channel)
        {
            _channel = channel;
        }

        public void PublishMessage(string exchangeName, string message, string? routingKey = null)
        {
            Console.WriteLine("------------------ Headers publisher ------------------");
            // Declaring headers dictionary
            var headers = new Dictionary<string, object>
            {
                { "x-message-headers", 30000 }
            };

            // ExchangeDeclare will create an exchange in RabbitMQ
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Headers, true, arguments: headers);

            // Since amqp is a binary protocol then we need to convert the message into bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Declaring properties to specify headers
            var properties = _channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object> { { "department", "dev" } };

            // Publish message to RabbitMQ
            // routingKey is the queue to which this message is going to be routed
            _channel.BasicPublish(exchangeName, routingKey, properties, bytes);
            Console.WriteLine($"Published Message: {message}");
        }
    }
}
