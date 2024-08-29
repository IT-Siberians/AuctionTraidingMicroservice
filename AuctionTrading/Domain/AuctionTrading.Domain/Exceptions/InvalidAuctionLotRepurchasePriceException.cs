using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionLotRepurchasePriceException(MoneyRUB repurchasePrice, MoneyRUB startPrice)
        : ArgumentException("The repurchase price cannot be less than or equal to the start price.")
    {
        public MoneyRUB RepurchasePrice => repurchasePrice;
        public MoneyRUB StartPrice => startPrice;
    }
}
