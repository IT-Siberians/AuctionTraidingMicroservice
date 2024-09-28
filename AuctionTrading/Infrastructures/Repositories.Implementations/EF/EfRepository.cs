using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.EF
{
    public class EfRepository<TEntity, TId>(ApplicationDbContext context) : IRepository<TEntity, TId>
                                                                        where TEntity : Entity<TId> where TId : struct
    {
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            context.Remove(entity);
            return context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                return;
            await DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => (await context.Set<TEntity>().ToListAsync()).AsEnumerable();


        public virtual async Task<TEntity?> GetByIdAsync(TId id) => await context.Set<TEntity>().FindAsync(id);


        public Task UpdateAsync(TEntity entity)
        {
            context.Update(entity);
            return context.SaveChangesAsync();
        }
    }
}
