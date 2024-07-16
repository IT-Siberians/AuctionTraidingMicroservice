using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Interfaces
{
    public interface ICustomer : IVisitor
    {
        Task MakeBidAsync(AuctionLot lot, decimal amount);
    }
}
