using AuctionTrading.Domain.Entities;

namespace AuctionTrading.Domain.Repositories.Abstractions
{
    public interface IAuctionLotRepository : IRepository<AuctionLot, Guid>
    {
        Task<IEnumerable<AuctionLot>> GetAllByEndDateAsync(DateTime endDateUtc);
    }
}