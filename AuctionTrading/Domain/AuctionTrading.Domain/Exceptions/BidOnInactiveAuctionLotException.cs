using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class BidOnInactiveAuctionLotException(AuctionLot lot)
        : InvalidOperationException($"Cannot bid on an inactive lot (id = {lot.Id}).")
    {
        public AuctionLot AuctionLot => lot;
    }
}
