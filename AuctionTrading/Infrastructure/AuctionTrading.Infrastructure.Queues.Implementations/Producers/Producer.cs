using AuctionTrading.Common.Infrastructure.Queues.Abstraction;
using MassTransit;

namespace AuctionTrading.Infrastructure.Queues.Implementations.Producers
{
    public class Producer<TModelEvent>(IPublishEndpoint publishEndpoint)
        : IProducerService<TModelEvent> where TModelEvent : class
    {
        public void Send(TModelEvent message)
        {
            publishEndpoint.Publish(message);
        }
    }
}
