using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionLotRepurchasePriceException(AuctionLot lot, MoneyRub repurchasePrice, MoneyRub startPrice)
        : ArgumentException($"The repurchase price of lot {lot.Title} cannot be less than or equal to the start price (lot id = {lot.Id}).")
    {
        public MoneyRub RepurchasePrice => repurchasePrice;
        public MoneyRub StartPrice => startPrice;
        public AuctionLot Lot => lot;
    }
}
