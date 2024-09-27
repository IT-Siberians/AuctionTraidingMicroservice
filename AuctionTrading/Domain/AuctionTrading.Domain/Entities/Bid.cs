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
        /// Get the bid creation time.
        /// </summary>
        public DateTime CreationTime { get; }

        /// <summary>
        /// Get the bid amount.
        /// </summary>
        public MoneyRub Amount { get; }

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
        /// <param name="creationTime">Date of bid creation</param>
        public Bid(Customer customer, AuctionLot lot, MoneyRub amount, DateTime creationTime)
            : this(Guid.NewGuid(), customer, lot, amount, creationTime)
        {

        }

        protected Bid(Guid id, Customer customer, AuctionLot lot, MoneyRub amount, DateTime creationTime)
            : base(id)
        {
            if (creationTime < lot.StartDate || creationTime > lot.EndDate)
                throw new InvalidCreationTimeBidException(lot, creationTime);

            if (!lot.IsActive)
                throw new BidOnInactiveAuctionLotException(lot, amount);

            Customer = customer ?? throw new ArgumentNullValueException(nameof(customer));
            Lot = lot ?? throw new ArgumentNullValueException(nameof(lot));
            Amount = amount ?? throw new ArgumentNullValueException(nameof(amount));
            CreationTime = creationTime;
        }
        #endregion // Constructor
    }
}
