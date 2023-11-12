using RabbitMQ.Client;

namespace RabbitMQConsumer
{
    public interface IRabbitMQConsumer
    {
        public void Consume(IModel channel);
    }
}
