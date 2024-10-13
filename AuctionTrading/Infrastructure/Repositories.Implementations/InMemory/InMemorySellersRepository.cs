using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemorySellersRepository(IEnumerable<Seller> sellers)
        : InMemoryRepository<Seller, Guid>(sellers), ISellersRepository
    {
        public InMemorySellersRepository() : this([])
        {

        }

        public Task<Seller?> GetSellerByUsernameAsync(string name)
            => Task.FromResult(_entities.FirstOrDefault(x => x.Username.Value == name));
    }
}
