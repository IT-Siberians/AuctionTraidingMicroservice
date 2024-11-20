using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfCustomerRepository(ApplicationDbContext context) 
        : EfRepository<Customer, Guid>(context), ICustomersRepository
    {
        private readonly DbSet<Customer> _customers = context.Set<Customer>();

        public override Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => _customers.Include("_observableAuctionLots")
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public Task<Customer?> GetCustomerByUsernameAsync(string username, CancellationToken cancellationToken) 
            => _customers.Include("_observableAuctionLots")
            .FirstOrDefaultAsync(s => s.Username.Equals(new Username(username)), cancellationToken);
    }
}