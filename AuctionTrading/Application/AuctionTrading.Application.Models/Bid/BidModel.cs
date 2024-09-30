using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Bid
{
    public class BidModel : IModel<Guid>
    {
        public required Guid Id { get; init; }

        public required DateTime CreationTime { get; init; }

        public required decimal Amount { get; init; }

        public required Guid AuctionLotId { get; init; }

        public required Guid CustomerId { get; init; }
    }
}
