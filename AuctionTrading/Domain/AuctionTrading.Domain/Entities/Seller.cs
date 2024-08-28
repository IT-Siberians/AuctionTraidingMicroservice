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
    public class Seller(Username username) : Entity<Guid>(Guid.NewGuid())
    {
        #region Fields
        /// <summary> 
        /// The seller's auction lots.
        /// </summary>
        private readonly ICollection<AuctionLot> _auctionLots = [];
        #endregion // Fields
        #region Properties
        /// <summary> 
        /// Gets the seller's Username. 
        /// </summary>
        public Username Username { get; private set; } = username;
        /// <summary>
        /// Gets the seller's active auction lots 
        /// </summary>
        public IReadOnlyCollection<AuctionLot> ActiveAuctionLots =>
            _auctionLots.Where(lot => lot.IsActive).ToList().AsReadOnly();
        #endregion // Properties
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
        /// <returns>true if the lot is successfully canceled; otherwise false.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CancelLot(AuctionLot lot)
        {
            var canceledLot = _auctionLots.SingleOrDefault(lot)
                ?? throw new AuctionLotDoesNotBelongToSellerException(this, lot);

            return canceledLot.CancelLot(this);
        }
    }
}
