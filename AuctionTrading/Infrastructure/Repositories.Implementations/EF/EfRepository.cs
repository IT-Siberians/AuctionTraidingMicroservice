using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfRepository<TEntity, TId>(ApplicationDbContext context)
        : IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => (await context.Set<TEntity>().ToListAsync()).AsEnumerable();

        public virtual async Task<TEntity?> GetByIdAsync(TId id)
            => await context.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity)
        {
            CheckEntity(entity);

            context.Add(entity);
            await context.SaveChangesAsync();
        }

        protected void CheckEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }
        public Task<bool> UpdateAsync(TEntity entity)
        {
            CheckEntity(entity);

            context.Update(entity);
            context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            CheckEntity(entity);

            context.Remove(entity);
            context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);

            return entity is null ? false : await DeleteAsync(entity);

        }
    }
}
