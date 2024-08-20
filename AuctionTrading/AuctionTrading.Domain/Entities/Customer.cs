using AuctionTrading.Domain.Base;
using AuctionTrading.Domain.Interfaces;
using AuctionTrading.Domain.ValueObject;
using System.Collections.Immutable;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the customer at the auction.
    /// </summary>
    public class Customer : Entity<Guid>, ICustomer
    {
        #region Fields
        /// <summary> 
        /// The customer's observable auction lots.
        /// </summary>
        private readonly IEnumerable<AuctionLot> _observableAuctionLots;
        #endregion // Fields
        #region Properties
        /// <summary> 
        /// Gets the customer's Username. 
        /// </summary>
        public Username Username { get; private set; }
        #endregion // Properties
        #region Constructors
        /// <summary>
        /// Initializes a new instance of a <see cref="Customer"></see> class that has observable lots.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <param name="username">The username of the customer.</param>
        /// <param name="observableAuctionLots">The observable auction lots of the customer.</param>
        public Customer(Guid id, Username username, IEnumerable<AuctionLot> observableAuctionLots)
            : base(id)
        {
            Username = username;
            _observableAuctionLots = observableAuctionLots;
        }
        #endregion // Constructors
        /// <summary> 
        /// Changes the customer's username. 
        /// </summary>
        /// <param name="newUsername">New customer's username.</param>
        internal void ChangeUsername(Username newUsername)
        {
            if (Username == newUsername) return;
            Username = newUsername;
        }
        /// <summary>
        /// Gets the read-only collection of customer's observable auction lots.
        /// </summary>
        /// <returns>A read-only collection of customer's observable auction lots.</returns>
        public IReadOnlyCollection<AuctionLot> GetAllLots()
        {
            return _observableAuctionLots.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the last bid of the seller's auction lot.
        /// </summary>
        /// <param name="auctionLot">An auction lot.</param>
        /// <returns>A last bid.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Bid? GetLastBid(AuctionLot auctionLot)
        {
            return auctionLot.LastBid;
        }
        /// <summary>
        /// Adds the auction lot to the sequence of observable lots.
        /// </summary>
        /// <param name="lot">A observable auction lot.</param>
        public void AddObservableLot(AuctionLot lot)
        {
            if (_observableAuctionLots.Contains(lot))
                return;
            _observableAuctionLots.Append(lot);
        }
        /// <summary>
        /// Bid on the auction lot.
        /// </summary>
        /// <param name="lot">Auction lot to be bid on.</param>
        /// <param name="amount">The bid amount.</param>
        /// <returns>true if was successfully make a bid otherwise false.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool TryMakeBid(AuctionLot lot, Money amount)
        { 
            if (!lot.IsActive)
                throw new InvalidOperationException();
            // Как проверить, вдруг лот принадлежит покупателю??
            Bid newBid = new Bid(this, lot, amount);
            return lot.TryAddBid(newBid);
        }
    }
}
