// See https://aka.ms/new-console-template for more information
using RabbitMQExamples;

Console.WriteLine("RabbitMQ Producer");

RabbitMQConnectionFactory factory = new RabbitMQConnectionFactory();

// Fanout Exchange
var channel = factory.CreateChannel();
var fanoutExchange = new RabbitMQFanout(channel);
var message = $"[Fanout] This is RabbitMQ message test at {DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")} - {DateTime.Now.Ticks}";
fanoutExchange.PublishMessage("webappFanoutExchange", message, null);

// Direct Exchange
var channelDirect = factory.CreateChannel();
var directExchange = new RabbitMQDirect(channelDirect);
var messageDirect = $"[Direct] This is RabbitMQ message test at {DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")} - {DateTime.Now.Ticks}";
var routingKey = "key.direct";
directExchange.PublishMessage("webappDirectExchange", messageDirect, routingKey);

// Headers Exchange
var channelHeaders = factory.CreateChannel();
var headersExchange = new RabbitMQHeaders(channelHeaders);
var messageHeaders = $"[Headers] This is RabbitMQ message test at {DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")} - {DateTime.Now.Ticks}";
headersExchange.PublishMessage("webappHeadersExchange", messageHeaders, "");

// Topic Exchange
var channelTopic = factory.CreateChannel();
var topicExchange = new RabbitMQTopic(channelTopic);
var messageTopic = $"[Topic] This is RabbitMQ message test at {DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss")} - {DateTime.Now.Ticks}";
var topic = "topic.demo";
topicExchange.PublishMessage("webappTopicExchange", messageTopic, topic);

factory.Dispose();