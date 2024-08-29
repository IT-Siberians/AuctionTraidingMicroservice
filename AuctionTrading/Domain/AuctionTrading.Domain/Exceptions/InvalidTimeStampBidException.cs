using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidTimeStampBidException(AuctionLot lot, DateTime timestamp)
        : ArgumentException($"The bid time {timestamp} does not fall within the auction period for the lot (id = {lot.Id}).")
    {
        public AuctionLot AuctionLot => lot;
        public DateTime Timestamp => timestamp;
    }
}
