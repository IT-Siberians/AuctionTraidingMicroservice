namespace AuctionTrading.Common.Infrastructure.Queues.Abstraction
{
    public interface IProducerService<TModelEvent>
    {
        void Send(TModelEvent message);
    }
}
