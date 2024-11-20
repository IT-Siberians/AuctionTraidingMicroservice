namespace AuctionTrading.Common.Infrastructure.Queues.Abstraction
{
    public interface IQueue<T>
    {
        public T QueueName { get; }
    }
}
