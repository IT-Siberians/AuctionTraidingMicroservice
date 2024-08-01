using AuctionTrading.Domain.Base;
using AuctionTrading.Domain.ValueObject;

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
        public DateTime Timestamp => DateTime.Now;
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
        public Bid(Customer customer, AuctionLot lot, Money amount)
        {
            Customer = customer;
            Lot = lot;
            Amount = amount;
        }
        #endregion // Constructor
    }
}
