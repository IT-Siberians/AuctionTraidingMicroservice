using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Exceptions
{
    public class InvalidAuctionTimePeriodException(AuctionLot lot, DateTime startDate, DateTime endDate)
        : ArgumentException($"The start of the auction for a lot {lot.Title} cannot be later than or coincide with the end of the auction (lot id = {lot.Id}).")
    {
        public DateTime StartDate => startDate;
        public DateTime EndDate => endDate;
        public AuctionLot Lot => lot;
    }
}
