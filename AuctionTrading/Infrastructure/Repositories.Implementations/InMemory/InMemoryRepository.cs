using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Repositories.Abstractions;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemoryRepository<TEntity, TId>(IEnumerable<TEntity> entities)
        : IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        protected readonly List<TEntity> Entities = entities.ToList();

        public InMemoryRepository() : this([])
        {

        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            Entities.Add(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            return Task.CompletedTask;

        }
        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                return;
            await DeleteAsync(entity);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync() 
            => Task.FromResult(Entities.AsEnumerable());

        public Task<TEntity?> GetByIdAsync(TId id) 
            => Task.FromResult(Entities.FirstOrDefault(x => x.Id.Equals(id)));

        public Task UpdateAsync(TEntity entity)
        {
            var entityToUpdate = Entities.FirstOrDefault(x => x.Id.Equals(entity.Id));
            if (entityToUpdate is null)
                return Task.CompletedTask;
            var index = Entities.IndexOf(entityToUpdate);
            Entities[index] = entity;
            return Task.CompletedTask;
        }
    }
}
