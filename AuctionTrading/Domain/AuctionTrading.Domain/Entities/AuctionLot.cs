using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Enums;
using AuctionTrading.Domain.Exceptions;
using AuctionTrading.Domain.ValueObjects;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the auction lot.
    /// </summary>
    public class AuctionLot : Entity<Guid>
    {
        #region Fields

        /// <summary>
        /// Collection of bids on the auction lot.
        /// </summary>
        private readonly ICollection<Bid> _bids = [];

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Get the title of the auction lot.
        /// </summary>
        public Title Title { get; }

        /// <summary>
        /// Get the description of the auction lot.
        /// </summary>
        public Description Description { get; }

        /// <summary>
        /// Get the start price of the auction lot.
        /// </summary>
        public MoneyRub StartPrice { get; }

        /// <summary>
        /// Get the fixed bid of the auction lot.
        /// </summary>
        public MoneyRub BidIncrement { get; }

        /// <summary>
        /// Get the repurchase price of the auction lot.
        /// </summary>
        public MoneyRub? RepurchasePrice { get; }

        /// <summary>
        /// Get the start date of the auction by lot. 
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Get the end date of the auction by lot.
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Get the status of the auction lot.
        /// </summary>
        public LotStatus Status { get; private set; }

        /// <summary>
        /// Get the seller of the auction lot.
        /// </summary>
        public Seller Seller { get; }

        /// <summary>
        /// Returns a value indicating whether the lot is currently being bid on.
        /// </summary>
        public bool IsActive => Status == LotStatus.Active;

        /// <summary>
        /// Get the last bid of the auction lot.
        /// </summary>
        public Bid? LastBid => _bids.Any() ? _bids.MaxBy(i => i.CreationTime) : null;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of a <see cref="AuctionLot"></see> class.
        /// </summary>
        /// <param name="title">The title of the auction lot.</param>
        /// <param name="description">The description of the auction lot.</param>
        /// <param name="startPrice">The start price of the auction lot.</param>
        /// <param name="bidIncrement">The fixed bid of the auction lot.</param>
        /// <param name="repurchasePrice">The repurchase price of the auction lot.</param>
        /// <param name="startDate">The start date of the auction lot.</param>
        /// <param name="endDate">The end date of the auction lot.</param>
        /// <param name="status">The status of the auction lot.</param>
        /// <param name="seller">The seller of the auction lot.</param>
        public AuctionLot(
            Guid id,
            Title title,
            Description description,
            MoneyRub startPrice,
            MoneyRub bidIncrement,
            MoneyRub? repurchasePrice,
            DateTime startDate,
            DateTime endDate,
            LotStatus status,
            Seller seller) : base(id)
        {
            if (repurchasePrice != null && repurchasePrice <= startPrice)
            {
                throw new InvalidAuctionLotRepurchasePriceException(this, repurchasePrice, startPrice);
            }

            if (endDate <= startDate)
            {
                throw new InvalidAuctionTimePeriodException(this, startDate, endDate);
            }

            Title = title ?? throw new ArgumentNullValueException(nameof(title));
            Description = description ?? throw new ArgumentNullValueException(nameof(description));
            StartPrice = startPrice ?? throw new ArgumentNullValueException(nameof(startPrice));
            BidIncrement = bidIncrement ?? throw new ArgumentNullValueException(nameof(bidIncrement));
            Seller = seller ?? throw new ArgumentNullValueException(nameof(seller));
            RepurchasePrice = repurchasePrice;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

        #endregion // Constructors

        /// <summary>
        /// Cancels an auctioned lot.
        /// </summary>
        /// <param name="seller">Seller of the lot.</param>
        /// <returns>true if the lot is successfully canceled; otherwise false.</returns>
        /// <exception cref="AnotherSellerCancelLotException"></exception>
        /// <exception cref="CancelNotActiveAuctionLotException"></exception>
        internal bool SetCancel(Seller seller)
        {
            if (Seller != seller)
                throw new AnotherSellerCancelLotException(this, seller);

            if (!IsActive)
                throw new CancelNotActiveAuctionLotException(this);

            if (_bids.Any())
                return false;

            Status = LotStatus.Canceled;
            return true;
        }

        internal bool SetComplete()
        {
            if (!IsActive)
                throw new CompletedNotActiveAuctionLotException(this);

            if (!(RepurchasePrice != null
                && LastBid != null
                && LastBid.Amount == RepurchasePrice
                || EndDate == DateTime.UtcNow))
                return false;

            Status = LotStatus.Completed;
            return true;

        }

        /// <summary>
        /// Adds the bid to the sequence of bids.
        /// </summary>
        /// <param name="newBid">The new bid on the lot.</param>
        /// <returns>true if the bid is successfully added to the bid sequence; otherwise false.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal BidStatus MakeBid(Bid newBid)
        {
            if (!IsActive)
            {
                if (Status == LotStatus.Canceled)
                    return BidStatus.FaultedLotWasCancel;

                if (Status == LotStatus.Completed)
                    return BidStatus.FaultedLotWasPurchased;
            }

            if (newBid.Lot != this)
                throw new AnotherAuctionLotBidException(this, newBid);

            if (_bids.Contains(newBid))
                throw new DoubleBidOnAuctionLotException(this, newBid);

            bool isCorrectBid = IsCorrectBid(newBid);
            if (isCorrectBid)
            {
                _bids.Add(newBid);
                SetComplete();
            }
            return isCorrectBid == true ? BidStatus.Success : BidStatus.FaultedIncorrectBid;
        }
        /// <summary>
        /// Checks the correctness of a bid on a lot.
        /// </summary>
        /// <param name="newBid">The new bid on the lot.</param>
        /// <returns>true if the bid is correctly; otherwise false.</returns>
        private bool IsCorrectBid(Bid newBid)
        {
            MoneyRub minAmount = LastBid == null
                ? StartPrice + BidIncrement
                : newBid.Amount + BidIncrement;
            return (newBid.Amount >= minAmount && newBid.CreationTime < EndDate);
        }
    }
}
