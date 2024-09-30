namespace AuctionTrading.Application.Models.Customer
{
    public class BidOnTheAuctionLotModel
    {
        public required Guid AuctionLotId { get; init; }

        public required decimal Bid { get; init; }
    }
}
