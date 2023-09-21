using BookSwap.Shared.Core.Modules.Entities;

namespace BookSwap.Shared.Core.Data.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> GetByIdAsync(Guid id, bool throwExceptionWhenNull = true);
    Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filters = null);
    Task<bool> AnyAsync(Func<TEntity, bool>? filters = null);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);
    Task<TEntity> DeleteAsync(TEntity entity);
}
