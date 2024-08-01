using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionTrading.Domain.Interfaces;

namespace AuctionTrading.Domain.Entities
{
    public class Visitor : IVisitor
    {
        public IReadOnlyCollection<AuctionLot> GetAllLots()
        {
            throw new NotImplementedException();
        }

        public Bid? GetLastBid(AuctionLot auctionLot)
        {
            throw new NotImplementedException();
        }

        public AuctionLot GetLotDetails(AuctionLot lot)
        {
            throw new NotImplementedException();
        }
    }
}
