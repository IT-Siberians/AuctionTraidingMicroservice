namespace AuctionTrading.Domain.Exception
{
    public class InvalidAuctionTimePeriodException(DateTime startDate, DateTime endDate)
        : ArgumentException("The start of the auction cannot be later than or coincide with the end of the auction.")
    {
        public DateTime StartDate => startDate;
        public DateTime EndDate => endDate;
    }
}
