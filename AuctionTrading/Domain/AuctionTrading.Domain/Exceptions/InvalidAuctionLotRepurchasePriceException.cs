using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionLotRepurchasePriceException(MoneyRub repurchasePrice, MoneyRub startPrice)
        : ArgumentException("The repurchase price cannot be less than or equal to the start price.")
    {
        public MoneyRub RepurchasePrice => repurchasePrice;
        public MoneyRub StartPrice => startPrice;
    }
}
