using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Entities.Base;
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
        public async Task<IEnumerable<AuctionLot>> GetAllByEndDateAsync(
            DateTime endDateUtc,
            CancellationToken cancellationToken,
        bool asNoTracking = false)
            => await (asNoTracking ? _auctionLots.AsNoTracking() : _auctionLots)
            .Where((x) => x.EndDate < endDateUtc.ToUniversalTime())
            .ToListAsync(cancellationToken);

        public override Task<AuctionLot?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => _auctionLots
            .Include(lot => lot.Seller)
            .Include("_bids")
            .FirstOrDefaultAsync(lot => lot.Id == id, cancellationToken);

    }
}
