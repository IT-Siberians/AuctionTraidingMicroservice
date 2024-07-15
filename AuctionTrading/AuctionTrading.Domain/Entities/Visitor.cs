using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Entities
{
    public class Visitor : IVisitor
    {
        public Task<IEnumerable<AuctionLot>> GetAllLotsAsync(IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }

        public Task<Bid> GetLastBidAsync(Guid lotId, IEnumerable<Bid> bids)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionLot> GetLotDetailsAsync(Guid lotId, IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }
    }
}
