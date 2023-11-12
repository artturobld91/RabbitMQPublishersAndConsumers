﻿using RabbitMQ.Client;

namespace RabbitMQConsumer
{
    /// <summary>
    /// Best practice is to reuse connections and multiplex a connection between threads with channels
    /// Seems that every producer and consumer needs to have it's own connection to RabbitMQ.
    /// AMQP connections: 7 TCP packages
    /// AMQP channel: 2 TCP packages
    /// AMQP publish: 1 TCP package(more for larger messages)
    /// AMQP close channel: 2 TCP packages
    /// AMQP close connection: 2 TCP packages
    /// Total 14–19 packages(+ Acks)
    /// </summary>
    public class RabbitMQConnectionFactory
    {
        private const string RabbitMQ_URL = "amqp://guest:guest@localhost:5672";
        private IConnection rabbitMQConnection = null;
        private IModel rabbitMQModel = null;
        public RabbitMQConnectionFactory()
        {

        }

        private IConnection CreateConnection() 
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(RabbitMQ_URL);
            rabbitMQConnection = factory.CreateConnection();
            return rabbitMQConnection;
        }

        public IModel CreateChannel()
        {
            CreateConnection();
            rabbitMQModel = rabbitMQConnection.CreateModel();
            return rabbitMQModel;
        }

        public void Dispose()
        {
            if (rabbitMQConnection != null) 
            {
                rabbitMQConnection.Close();
                rabbitMQModel.Close();
            }
        }
       
    }
}
