using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Enums;
using AuctionTrading.Domain.Exceptions;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the customer at the auction.
    /// </summary>
    public class Customer(Guid id, Username username) : Entity<Guid>(id)
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
        public Username Username { get; private set; } = username ?? throw new ArgumentNullValueException(nameof(username));

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
        internal bool ChangeUsername(Username newUsername)
        {
            if (Username == newUsername) return false;
            Username = newUsername;
            return true;
        }

        /// <summary>
        /// Adds the auction lot to the sequence of observable lots.
        /// </summary>
        /// <param name="lot">A observable auction lot.</param>
        public void AddObservableLot(AuctionLot lot)
        {
            if (_observableAuctionLots.Contains(lot))
                return;
            _observableAuctionLots.Add(lot);
        }

        /// <summary>
        /// Bid on the auction lot.
        /// </summary>
        /// <param name="lot">Auction lot to be bid on.</param>
        /// <param name="amount">The bid amount.</param>
        /// <returns>true if was successfully make a bid otherwise false.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public BidStatus TryMakeBid(AuctionLot lot, Money amount)
        {
            if (Id == lot.Seller.Id)
                return BidStatus.FaultedCreateBidOnYourLot;

            switch (lot.Status)
            {
                case LotStatus.Canceled:
                    return BidStatus.FaultedLotWasCancel;
                case LotStatus.Completed:
                    return BidStatus.FaultedLotWasPurchased;
                case LotStatus.Active:
                    return MakeBid(lot, amount);
                default:
                    throw new NotForeseenSituationForThisLotStatusException(lot, lot.Status);
            }
        }

        private BidStatus MakeBid(AuctionLot lot, Money amount)
        {
            Bid newBid = new Bid(this, lot, amount, DateTime.UtcNow);
            BidStatus bidStatus = lot.MakeBid(newBid);
            if (bidStatus == BidStatus.Success)
                AddObservableLot(lot);
            return bidStatus;
        }
    }
}
