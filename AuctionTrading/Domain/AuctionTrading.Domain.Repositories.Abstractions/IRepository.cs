using AuctionTrading.Domain.Entities.Base;

namespace AuctionTrading.Domain.Repositories.Abstractions
{
    public interface IRepository<TEntity, in TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken);

    }
}
