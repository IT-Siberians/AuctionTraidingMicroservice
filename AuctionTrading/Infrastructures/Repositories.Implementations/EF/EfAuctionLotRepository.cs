using AuctionTrading.Domain.Entities;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{

    public class EfAuctionLotRepository(ApplicationDbContext context) : EfRepository<AuctionLot, Guid>(context)
    {
        private readonly DbSet<AuctionLot> _auctionLots = context.AuctionLots;
        public override Task<AuctionLot?> GetByIdAsync(Guid id)
            => _auctionLots
            .Include(l => l.Seller)
            .Include(l => l.LastBid)
            .FirstOrDefaultAsync(l => l.Id == id);

    }
}
