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

        public override Task<Seller?> GetByIdAsync(Guid id)
            => _sellers.Include(s => s.ActiveAuctionLots)
            .FirstOrDefaultAsync(s => s.Id == id);

        public Task<Seller?> GetSellerByUsernameAsync(string username)
            => _sellers.Include(s => s.ActiveAuctionLots)
            .FirstOrDefaultAsync(s => s.Username.Value == username);
    }
}
