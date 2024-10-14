using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{

    public class EfAuctionLotRepository(ApplicationDbContext context)
        : EfRepository<AuctionLot, Guid>(context), IAuctionLotRepository
    {
        private readonly DbSet<AuctionLot> _auctionLots = context.Set<AuctionLot>();


        // У меня большой вопрос, как сделать правильный асинхронный метод GetAllByEndDateAsync?
        public async Task<IEnumerable<AuctionLot>> GetAllByEndDateAsync(DateTime endDateUtc)
            => await _auctionLots.Where((x) => x.EndDate < endDateUtc.ToUniversalTime()).ToListAsync();

        public override Task<AuctionLot?> GetByIdAsync(Guid id)
                => _auctionLots
                .Include(l => l.Seller)
                .Include(l => l.LastBid)
                .FirstOrDefaultAsync(l => l.Id == id);

    }
}
