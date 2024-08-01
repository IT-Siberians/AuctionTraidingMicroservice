using AuctionTrading.Domain.Base;
using AuctionTrading.Domain.Enums;
using AuctionTrading.Domain.Interfaces;
using AuctionTrading.Domain.ValueObject;
using AuctionTrading.Domain.Exception;
using System.Net.Http.Headers;
using System.Collections.Immutable;

namespace AuctionTrading.Domain.Entities
{
    /// <summary>
    /// Represents the seller of lots at the auction.
    /// </summary>
    public class Seller : Entity<Guid>, ISeller
    {
        #region Fields
        /// <summary> 
        /// The seller's auction lots.
        /// </summary>
        private readonly IEnumerable<AuctionLot> _auctionLots;
        #endregion // Fields
        #region Properties
        /// <summary> 
        /// Gets the seller's Username. 
        /// </summary>
        public Username Username { get; private set; }
        #endregion // Properties
        #region Constructors
        /// <summary>
        /// Initializes a new instance of a <see cref="Seller"></see> class that has lots up for auction.
        /// </summary>
        /// <param name="id">The ID of the seller.</param>
        /// <param name="username">The username of the seller.</param>
        /// <param name="auctionLots">The auction lots of the seller.</param>
        public Seller(Guid id, Username username, IEnumerable<AuctionLot> auctionLots)
            : base(id)
        {
            Username = username;
            _auctionLots = auctionLots;
        }
        #endregion // Constructors
        /// <summary> 
        /// Changes the seller's username. 
        /// </summary>
        /// <param name="newUsername">New seller's username.</param>
        internal void ChangeUsername(Username newUsername)
        {
            if (Username == newUsername) return;
            Username = newUsername;
        }
        /// <summary>
        /// Cancels an auctioned lot.
        /// </summary>
        /// <param name="lot">Lot to be withdrawn from auction</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void CancelLot(AuctionLot lot)
        {
            if (lot.Seller!=this)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_CANCEL_LOT_ANOTHER_SELLER);
            if (!lot.IsActive)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_CANCEL_NOT_ACTIVE_LOT);
            var canceledLot = _auctionLots.SingleOrDefault(lot);
            if (canceledLot==null)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_CANCEL_LOT_EMPTY_SEQUENCE);
            // Думаю стоит добавить отслеживание времени. Например нельзя отменить лот за час до окончания торгов
            canceledLot.ChangeStatus(LotStatus.Canceled);
        }
        /// <summary>
        /// Gets the read-only collection of seller's auction lots.
        /// </summary>
        /// <returns>A read-only collection of seller's auction lots.</returns>
        public IReadOnlyCollection<AuctionLot> GetAllLots()
        {
            return _auctionLots.ToImmutableList();
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
        /// Gets the lot assigned to the seller.
        /// </summary>
        /// <param name="lot">A auction lot assigned to the seller.</param>
        /// <returns>Auction lot</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public AuctionLot GetLotDetails(AuctionLot lot)
        {
            if (lot.Seller != this)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_GET_LOT_ANOTHER_SELLER);
            if (!lot.IsActive)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_GET_NOT_ACTIVE_LOT);
            var auctionLot = _auctionLots.SingleOrDefault(lot);
            if (auctionLot == null)
                throw new InvalidOperationException(ExceptionMessage.CANNOT_GET_LOT_EMPTY_SEQUENCE);
            return auctionLot;
        }
    }
}
