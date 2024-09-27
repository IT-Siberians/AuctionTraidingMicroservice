using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class DoubleBidOnAuctionLotException(AuctionLot lot, Bid bid)
        : InvalidOperationException($"Auction lot {lot.Title} has been bid {bid.Amount} (lot id = {lot.Id})")
    {
        public AuctionLot AuctionLot => lot;
        public Bid Bid => bid;
    }
}
