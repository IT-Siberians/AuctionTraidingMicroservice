using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfCustomerRepository(ApplicationDbContext context) : EfRepository<Customer, Guid>(context), ICustomersRepository
    {
        private readonly DbSet<Customer> _customers = context.Set<Customer>();

        public override Task<Customer?> GetByIdAsync(Guid id) => _customers.Include(c => c.ObservableAuctionLots)
                                                                .FirstOrDefaultAsync(c => c.Id == id);

        public Task<Customer?> GetCustomerByUsernameAsync(string username) => _customers.Include(s => s.ObservableAuctionLots)
                                                                .FirstOrDefaultAsync(s => s.Username.Value == username);


    }
}