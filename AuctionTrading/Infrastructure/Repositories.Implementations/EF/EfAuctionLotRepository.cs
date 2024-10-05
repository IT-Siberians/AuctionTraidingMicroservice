using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{

    public class EfAuctionLotRepository(ApplicationDbContext context)
        : EfRepository<AuctionLot, Guid>(context), IAuctionLotRepository
    {
        private readonly DbSet<AuctionLot> _auctionLots = context.AuctionLots;

        public async Task<IEnumerable<AuctionLot>> GetAllByEndDateAsync(DateTime endDateUtc)
        {
            var results = new ConcurrentQueue<AuctionLot>();
            var tasks = _auctionLots.Select(
                async x =>
                {
                    if (await _auctionLots.FirstOrDefaultAsync((x)=>x.EndDate==endDateUtc))
                        results.Enqueue(x);
                });
            await Task.WhenAll(tasks);
            return results;
        }


        public override Task<AuctionLot?> GetByIdAsync(Guid id)
                => _auctionLots
                .Include(l => l.Seller)
                .Include(l => l.LastBid)
                .FirstOrDefaultAsync(l => l.Id == id);

    }
}
