using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObject;

namespace AuctionTrading.Domain.Interfaces
{
    public interface ICustomer : IVisitor
    {
        bool TryMakeBid(AuctionLot lot, Money amount);
        void AddObservableLot(AuctionLot lot);
    }
}
