using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Interfaces
{
    internal interface ISeller : IVisitor
    {
        void CancelLot(AuctionLot lot);
    }
}
