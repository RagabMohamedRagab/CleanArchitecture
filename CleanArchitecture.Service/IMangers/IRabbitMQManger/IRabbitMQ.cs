using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.IMangers.IRabbitMQManger
{
    public interface IRabbitMQ
    {
        Task Producer(string Message, string QueueName);
        Task SubscribeAsync(string queueName);
    }
}
