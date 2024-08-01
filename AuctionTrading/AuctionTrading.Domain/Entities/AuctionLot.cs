using AuctionTrading.Domain.Base;
using AuctionTrading.Domain.Enums;
using AuctionTrading.Domain.ValueObject;
using System.Diagnostics;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the auction lot.
    /// </summary>
    public class AuctionLot : Entity<Guid>
    {
        #region Fields
        /// <summary>
        /// Sequence of bids on the auction lot.
        /// </summary>
        private readonly IEnumerable<Bid> _bids;
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
        public Money StartPrice { get; }
        /// <summary>
        /// Get the fixed bid of the auction lot.
        /// </summary>
        public Money FixedBid { get; }
        /// <summary>
        /// Get the repurchase price of the auction lot.
        /// </summary>
        public Money? RepurchasePrice { get; }
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
        public Bid? LastBid => _bids.MaxBy(i => i.Timestamp);
        #endregion // Properties

        public AuctionLot(Guid id, Title title, Description description, 
            Money startPrice, Money fixedBid, Money? repurchasePrice, 
            DateTime startDate, DateTime endDate, LotStatus status, Seller seller, IEnumerable<Bid> bids) 
            : base(id)
        {
            if (repurchasePrice!=null && repurchasePrice < startPrice)
            {
                throw new ArgumentException("RepurchasePrice must be greater than startPrice.");
            }

            if (endDate <= startDate)
            {
                throw new ArgumentException("EndDate must be greater than StartDate.");
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
            _bids = bids;
        }
        public void ChangeStatus(LotStatus newLotStatus)
        {
            throw new NotImplementedException();
        }
        public bool TryAddBid(Bid newBid)
        {
            if (!this.IsActive)
                throw new InvalidOperationException();
            if (newBid.Lot != this)
                throw new InvalidOperationException();
            if (!_bids.Contains(newBid))
                throw new InvalidOperationException();
            bool flag = IsCurrentBid(newBid);
            if (flag) _bids.Append(newBid);
            return flag;
        }
        public bool IsCurrentBid(Bid newBid)
        {
            Money minAmount = this.LastBid == null ?
                    this.StartPrice + this.FixedBid :
                    newBid.Amount + this.FixedBid;
            return (newBid.Amount > minAmount && newBid.Timestamp < this.EndDate) ? true : false;


        }
    }
}
