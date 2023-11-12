using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExamples
{
    public interface IRabbitMQExchange
    {
        void PublishMessage(string exchangeName, string message, string? routingKey = null);
    }
}
