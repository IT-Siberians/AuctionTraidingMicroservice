using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionLotRepurchasePriceException(Money repurchasePrice, Money startPrice)
        : ArgumentException("The repurchase price cannot be less than or equal to the start price.")
    {
        public Money RepurchasePrice => repurchasePrice;
        public Money StartPrice => startPrice;
    }
}
