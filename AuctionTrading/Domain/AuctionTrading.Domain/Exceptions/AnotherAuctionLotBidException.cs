using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class AnotherAuctionLotBidException(AuctionLot lot, Bid bid)
        : InvalidOperationException($"The bid on lot {bid.AuctionLot.Title} cannot be placed on lot {lot.Title} (id = {lot.Id})")
    {
        public AuctionLot AuctionLot => lot;
        public Bid Bid => bid;
    }
}
