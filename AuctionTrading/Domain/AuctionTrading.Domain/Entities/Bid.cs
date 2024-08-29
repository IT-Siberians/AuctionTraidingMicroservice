using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Exceptions;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the bid for the auction lot.
    /// </summary>
    public class Bid : Entity<Guid>
    {
        #region Properties
        /// <summary>
        /// Get the bid time.
        /// </summary>
        public DateTime Timestamp { get; }
        /// <summary>
        /// Get the bid amount.
        /// </summary>
        public Money Amount { get; }
        /// <summary>
        /// Get the lot on which the bid has been placed.
        /// </summary>
        public AuctionLot Lot { get; }
        /// <summary>
        /// Get the customer who has bid on the lot.
        /// </summary>
        public Customer Customer { get; }
        #endregion // Properties
        #region Constructor
        /// <summary>
        /// Initializes a new instance of a <see cref="Bid"></see> class.
        /// </summary>
        /// <param name="customer">The customer who has bid on the lot.</param>
        /// <param name="lot">The lot on which the bid has been placed.</param>
        /// <param name="amount">The bid amount.</param>
        public Bid(Customer customer, AuctionLot lot, Money amount, DateTime timestamp)
            : this(Guid.NewGuid(), customer, lot, amount, timestamp)
        {

        }
        protected Bid(Guid id, Customer customer, AuctionLot lot, Money amount, DateTime timestamp)
        {
            if (timestamp.ToUniversalTime() < lot.StartDate.ToUniversalTime()
                || timestamp.ToUniversalTime() > lot.EndDate.ToUniversalTime())
                throw new InvalidTimeStampBidException(lot, timestamp);
            if (!lot.IsActive)
                throw new BidOnInactiveAuctionLotException(lot);

            Customer = customer;
            Lot = lot;
            Amount = amount;
            Timestamp = timestamp;
        }
        #endregion // Constructor
    }
}
