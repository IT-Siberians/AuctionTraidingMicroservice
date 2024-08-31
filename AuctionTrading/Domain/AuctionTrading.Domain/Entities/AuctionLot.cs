﻿using AuctionTrading.Domain.Entities.Base;
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
        public MoneyRUB StartPrice { get; }
        /// <summary>
        /// Get the fixed bid of the auction lot.
        /// </summary>
        public MoneyRUB FixedBid { get; }
        /// <summary>
        /// Get the repurchase price of the auction lot.
        /// </summary>
        public MoneyRUB? RepurchasePrice { get; }
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
        public bool IsActive => this.Status == LotStatus.Active;
        /// <summary>
        /// Get the last bid of the auction lot.
        /// </summary>
        public Bid? LastBid => _bids.Any() ? _bids.MaxBy(i => i.Timestamp) : null;
        #endregion // Properties
        #region Constructors
        /// <summary>
        /// Initializes a new instance of a <see cref="AuctionLot"></see> class.
        /// </summary>
        /// <param name="title">The title of the auction lot.</param>
        /// <param name="description">The description of the auction lot.</param>
        /// <param name="startPrice">The start price of the auction lot.</param>
        /// <param name="fixedBid">The fixed bid of the auction lot.</param>
        /// <param name="repurchasePrice">The repurchase price of the auction lot.</param>
        /// <param name="startDate">The start date of the auction lot.</param>
        /// <param name="endDate">The end date of the auction lot.</param>
        /// <param name="status">The status of the auction lot.</param>
        /// <param name="seller">The seller of the auction lot.</param>
        public AuctionLot(Title title, Description description,
            MoneyRUB startPrice, MoneyRUB fixedBid, MoneyRUB? repurchasePrice,
            DateTime startDate, DateTime endDate, LotStatus status, Seller seller)
            : this(Guid.NewGuid(), title, description, startPrice, fixedBid, repurchasePrice, startDate, endDate, status, seller)
        {

        }
        protected AuctionLot(Guid id, Title title, Description description,
            MoneyRUB startPrice, MoneyRUB fixedBid, MoneyRUB? repurchasePrice,
            DateTime startDate, DateTime endDate, LotStatus status, Seller seller) : base(id)
        {
            if (repurchasePrice != null && repurchasePrice < startPrice)
            {
                throw new InvalidAuctionLotRepurchasePriceException(repurchasePrice, startPrice);
            }

            if (endDate.ToUniversalTime() <= startDate.ToUniversalTime())
            {
                throw new InvalidAuctionTimePeriodException(startDate, endDate);
            }

            Title = title;
            Description = description;
            StartPrice = startPrice;
            FixedBid = fixedBid;
            RepurchasePrice = repurchasePrice;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            Seller = seller;
        }
        #endregion // Constructors
        /// <summary>
        /// Cancels an auctioned lot.
        /// </summary>
        /// <param name="seller">Seller of the lot.</param>
        /// <returns>true if the lot is successfully canceled; otherwise false.</returns>
        /// <exception cref="AnotherSellerCancelLotException"></exception>
        /// <exception cref="CancelNotActiveAuctionLotException"></exception>
        internal bool CancelLot(Seller seller)
        {
            if (Seller != seller)
                throw new AnotherSellerCancelLotException(this, seller);
            if (!this.IsActive)
                throw new CancelNotActiveAuctionLotException(this);

            if (this._bids.Any())
                return false;
            this.Status = LotStatus.Canceled;
            return true;
        }
        /// <summary>
        /// Adds the bid to the sequence of bids.
        /// </summary>
        /// <param name="newBid">The new bid on the lot.</param>
        /// <returns>true if the bid is successfully added to the bid sequence; otherwise false.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal bool TryAddBid(Bid newBid)
        {
            if (!IsActive)
                throw new BidOnInactiveAuctionLotException(this, newBid.Amount);
            if (newBid.Lot != this)
                throw new AnotherAuctionLotBidException(this, newBid);
            if (!_bids.Contains(newBid))
                throw new DoubleBidOnAuctionLotException(this, newBid);

            bool isCurrentBid = IsCurrentBid(newBid);
            if (isCurrentBid) _bids.Append(newBid);
            return isCurrentBid;
        }
        /// <summary>
        /// Checks the correctness of a bid on a lot.
        /// </summary>
        /// <param name="newBid">The new bid on the lot.</param>
        /// <returns>true if the bid is correctly; otherwise false.</returns>
        private bool IsCurrentBid(Bid newBid)
        {
            MoneyRUB minAmount = LastBid == null
                ? StartPrice + FixedBid
                : newBid.Amount + FixedBid;
            return (newBid.Amount > minAmount && newBid.Timestamp < EndDate);
        }
    }
}
