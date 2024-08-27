using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exception
{
    public class AnotherAuctionLotBidException(AuctionLot lot, Bid bid)
        : InvalidOperationException("This bid cannot be placed on another lot")
    {
        public AuctionLot AuctionLot => lot;
        public Bid Bid => bid;
    }
}
