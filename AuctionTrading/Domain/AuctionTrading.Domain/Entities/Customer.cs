using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Exceptions;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the customer at the auction.
    /// </summary>
    public class Customer(Username username) : Entity<Guid>(Guid.NewGuid())
    {
        #region Fields
        /// <summary> 
        /// The customer's observable auction lots.
        /// </summary>
        private readonly ICollection<AuctionLot> _observableAuctionLots = [];
        #endregion // Fields
        #region Properties
        /// <summary> 
        /// Gets the customer's Username. 
        /// </summary>
        public Username Username { get; private set; } = username;
        /// <summary>
        /// Gets the read-only collection of customer's observable auction lots.
        /// </summary>
        public IReadOnlyCollection<AuctionLot> ObservableAuctionLots =>
            _observableAuctionLots.Where(lot => lot.IsActive).ToList().AsReadOnly();
        #endregion // Properties
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
            if (Id == lot.Seller.Id)
                return false;

            if (!lot.IsActive)
                throw new BidOnInactiveAuctionLotException(lot);

            Bid newBid = new Bid(this, lot, amount, DateTime.Now);
            if (lot.TryAddBid(newBid))
                AddObservableLot(lot);
            return lot.TryAddBid(newBid);
        }
    }
}
