﻿using RabbitMQ.Client;

namespace RabbitMQExamples
{
    public class RabbitMQFanout : IRabbitMQExchange
    {
        private IModel _channel;
        public RabbitMQFanout(IModel channel)
        {
            _channel = channel;
        }

        public void PublishMessage(string exchangeName, string message, string? routingKey)
        {
            Console.WriteLine("------------------ Fanout publisher ------------------");
            // ExchangeDeclare will create an exchange in RabbitMQ
            _channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);

            // Since amqp is a binary protocol then we need to convert the message into bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(message);

            // Publish message to RabbitMQ
            _channel.BasicPublish(exchangeName, "", null, bytes);

            Console.WriteLine($"Published Message: {message}");
        }
    }
}
