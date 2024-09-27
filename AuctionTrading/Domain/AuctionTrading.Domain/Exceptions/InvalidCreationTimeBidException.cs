using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidCreationTimeBidException(AuctionLot lot, DateTime creationTime)
        : ArgumentException($"The bid time {creationTime} does not fall within the auction period for the lot {lot.Title} (id = {lot.Id}).")
    {
        public AuctionLot AuctionLot => lot;
        public DateTime CreationTime => creationTime;
    }
}
