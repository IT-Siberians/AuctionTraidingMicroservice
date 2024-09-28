using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemoryCustomersRepository(IEnumerable<Customer> customers) : InMemoryRepository<Customer, Guid>(customers), ICustomersRepository
    {
        public InMemoryCustomersRepository() : this([])
        {

        }

        public Task<Customer?> GetCustomerByUsernameAsync(string username)
            => Task.FromResult(Entities.FirstOrDefault(x => x.Username.Value == username));
    }
}
