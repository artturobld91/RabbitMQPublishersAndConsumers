using RabbitMQ.Client;

namespace RabbitMQExamples
{
    public class RabbitMQTopic : IRabbitMQExchange
    {
        private IModel _channel;

        public RabbitMQTopic(IModel channel)
        {
            _channel = channel;
        }

        public void PublishMessage(string exchangeName, string message, string? routingKey = null)
        {
            Console.WriteLine("------------------ Topic publisher ------------------");
            
            // ExchangeDeclare will create an exchange in RabbitMQ
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);

            // Since amqp is a binary protocol then we need to convert the message into bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Declaring properties to specify headers
            var properties = _channel.CreateBasicProperties();

            // Publish message to RabbitMQ
            // routingKey is the topic to which will be used for this message to be routed
            _channel.BasicPublish(exchangeName, routingKey, properties, bytes);
            Console.WriteLine($"Published Message: {message}");
        }
    }
}
