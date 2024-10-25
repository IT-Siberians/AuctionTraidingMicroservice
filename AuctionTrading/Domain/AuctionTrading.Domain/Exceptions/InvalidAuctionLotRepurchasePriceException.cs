using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionLotRepurchasePriceException(AuctionLot lot, Money repurchasePrice, Money startPrice)
        : ArgumentOutOfRangeException($"The repurchase price of lot {lot.Title} cannot be less than or equal to the start price (lot id = {lot.Id}).")
    {
        public Money RepurchasePrice => repurchasePrice;
        public Money StartPrice => startPrice;
        public AuctionLot Lot => lot;
    }
}
