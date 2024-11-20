using AuctionTrading.Common.Enums;

namespace AuctionTrading.Infrastructure.RabbitMQ
{
    public class RabbitMqConfig
    {
        public required string ConnectionString { get; init; }
        public required string[] ProducerQueues { get; init; }
        public required string[] ConsumerQueues { get; init; }
    }
}
