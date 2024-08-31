using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class BidOnInactiveAuctionLotException(AuctionLot lot, MoneyRUB amount)
        : InvalidOperationException($"Cannot bid {amount} RUB on an inactive lot (id = {lot.Id}).")
    {
        public AuctionLot AuctionLot => lot;
        public MoneyRUB amount => amount;
    }
}
