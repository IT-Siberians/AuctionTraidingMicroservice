namespace AuctionTrading.Common.Infrastructure.Queues.Abstraction
{
    public interface IProducerService<TModelEvent>
    {
        Task Send(TModelEvent message);
    }
}
