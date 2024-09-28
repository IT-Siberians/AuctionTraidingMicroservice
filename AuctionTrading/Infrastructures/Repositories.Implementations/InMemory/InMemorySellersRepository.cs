using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using System.Text.RegularExpressions;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemorySellersRepository(IEnumerable<Seller> sellers) : InMemoryRepository<Seller, Guid>(sellers), ISellersRepository
    {
        public InMemorySellersRepository() : this([])
        {

        }

        public Task<Seller?> GetSellerByUsernameAsync(string name)
            => Task.FromResult(Entities.FirstOrDefault(x => x.Username.Value == name));

        public Task<Seller?> GetSellerByAuctionLotIdAsync(Guid auctionLotId)
           => Task.FromResult(Entities.FirstOrDefault(
                   x => x.ActiveAuctionLots.FirstOrDefault(lot => lot.Id == auctionLotId) is not null));
    }
}
