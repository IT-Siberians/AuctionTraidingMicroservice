using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class BidOnInactiveAuctionLotException(AuctionLot lot, MoneyRub amount)
        : InvalidOperationException($"Cannot bid {amount} Rub on an inactive lot {lot.Title} (id = {lot.Id}).")
    {
        public AuctionLot AuctionLot => lot;
        public MoneyRub amount => amount;
    }
}
