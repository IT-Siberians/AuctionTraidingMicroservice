using AuctionTrading.Domain.Entities.Base;
using AuctionTrading.Domain.Exceptions;
using AuctionTrading.Domain.Repositories.Abstractions;

namespace AuctionTrading.Infrastructure.Repositories.Implementations.InMemory
{
    public class InMemoryRepository<TEntity, TId>
        : IRepository<TEntity, TId> 
        where TEntity : Entity<TId> 
        where TId : struct, IEquatable<TId>
    {
        protected readonly List<TEntity> _entities;

        public InMemoryRepository() : this([])
        {

        }

        public InMemoryRepository(IEnumerable<TEntity> entities)
        {
            _entities = entities?.ToList()
                ?? throw new ArgumentNullValueException(nameof(entities));
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
            => Task.FromResult(_entities.AsEnumerable());

        public virtual Task<TEntity?> GetByIdAsync(TId id)
            => Task.FromResult(_entities.FirstOrDefault(x => x.Id.Equals(id)));

        public virtual Task AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            _entities.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task<bool> UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var existingEntity = _entities.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (existingEntity is null)
            {
                return Task.FromResult(false);
            }

            var index = _entities.IndexOf(existingEntity);
            _entities[index] = entity;

            return Task.FromResult(true);
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            return _entities.Remove(entity) ? Task.FromResult(true) : Task.FromResult(false);

        }
        public virtual async Task<bool> DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);

            return entity is null ? false : await DeleteAsync(entity);
        }
    }
}
