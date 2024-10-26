using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfSellerRepository(ApplicationDbContext context)
        : EfRepository<Seller, Guid>(context), ISellersRepository
    {
        private readonly DbSet<Seller> _sellers = context.Set<Seller>();

        public override Task<Seller?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => _sellers.Include("_auctionLots")
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        public Task<Seller?> GetSellerByUsernameAsync(string username, CancellationToken cancellationToken)
            => _sellers.Include("_auctionLots")
            .FirstOrDefaultAsync(s => s.Username.Value == username, cancellationToken);
    }
}
