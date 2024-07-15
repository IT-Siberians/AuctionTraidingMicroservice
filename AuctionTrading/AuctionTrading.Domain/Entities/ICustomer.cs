using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Entities
{
    public interface ICustomer : IVisitor
    {
        Task MakeBidAsync(AuctionLot lot, decimal amount);
    }
}
