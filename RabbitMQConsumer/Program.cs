// See https://aka.ms/new-console-template for more information
using RabbitMQConsumer;

Console.WriteLine("RabbitMQ Consumer");

RabbitMQConnectionFactory factory = new RabbitMQConnectionFactory();

var channel = factory.CreateChannel();
RabbitMQFanoutConsumer consumer = new RabbitMQFanoutConsumer();
consumer.Consume(channel);

var channelDirect = factory.CreateChannel();
RabbitMQDirectConsumer directConsumer = new RabbitMQDirectConsumer();
directConsumer.Consume(channelDirect);

var channelHeader = factory.CreateChannel();
RabbitMQHeadersConsumer headersConsumer = new RabbitMQHeadersConsumer();
headersConsumer.Consume(channelHeader);

var channelTopic = factory.CreateChannel();
RabbitMQTopicConsumer topicConsumer = new RabbitMQTopicConsumer();
topicConsumer.Consume(channelTopic);

factory.Dispose();
