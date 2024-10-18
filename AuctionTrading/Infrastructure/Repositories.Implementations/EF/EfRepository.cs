using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfRepository<TEntity, TId>(ApplicationDbContext context)
        : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : struct, IEquatable<TId>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => (await context.Set<TEntity>().ToListAsync());

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
            => await context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            context.Update(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            context.Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);

            return entity is null ? false : await DeleteAsync(entity);

        }
    }
}
