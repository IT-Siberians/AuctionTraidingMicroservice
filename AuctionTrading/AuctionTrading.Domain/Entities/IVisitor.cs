using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Entities
{
    public interface IVisitor
    {
        Task<IEnumerable<AuctionLot>> GetAllLotsAsync(IEnumerable<AuctionLot> lots);
        Task<AuctionLot> GetLotDetailsAsync(Guid lotId, IEnumerable<AuctionLot> lots);
        Task<Bid> GetLastBidAsync(Guid lotId, IEnumerable<Bid> bids);
    }
}
