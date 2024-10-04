namespace AuctionTrading.Application.Models.Customer
{
    public class BidOnAuctionLotModel
    {
        public required Guid AuctionLotId { get; init; }

        public required decimal Bid { get; init; }
    }
}
