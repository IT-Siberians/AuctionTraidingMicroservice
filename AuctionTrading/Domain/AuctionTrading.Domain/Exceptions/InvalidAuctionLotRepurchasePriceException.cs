using AuctionTrading.Domain.ValueObject;

namespace AuctionTrading.Domain.Exception
{
    public class InvalidAuctionLotRepurchasePriceException(Money repurchasePrice, Money startPrice)
        : ArgumentException("The repurchase price cannot be less than or equal to the start price.")
    {
        public Money RepurchasePrice => repurchasePrice;
        public Money StartPrice => startPrice;
    }
}
