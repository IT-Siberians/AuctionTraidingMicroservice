using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using System.Xml.Linq;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemoryAuctionLotsRepository(IEnumerable<AuctionLot> auctionLots)
        : InMemoryRepository<AuctionLot, Guid>(auctionLots), IAuctionLotRepository
    {
        public InMemoryAuctionLotsRepository() : this([])
        {
        }

        public Task<IEnumerable<AuctionLot>> GetAllByEndDateAsync(DateTime endDateUtc)
            => Task.FromResult(_entities.Where(x => x.EndDate < endDateUtc.ToUniversalTime()));
    }
}
