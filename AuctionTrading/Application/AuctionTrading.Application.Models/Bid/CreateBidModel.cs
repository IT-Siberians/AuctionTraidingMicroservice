using AuctionTrading.Application.Models.Base;

namespace AuctionTrading.Application.Models.Bid
{
    public class CreateBidModel : ICreateModel
    {
        public required decimal Amount { get; init; }

        public required Guid AuctionLotId { get; init; }

        public required Guid CustomerId { get; init; }
    }
}
