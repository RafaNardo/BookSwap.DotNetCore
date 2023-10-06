using BookSwap.Shared.Core.Data.Specifications;
using BookSwap.Shared.Core.Models;

namespace BookSwap.Shared.Core.Data.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> FindAsync(Guid id, bool throwExceptionWhenNull = true);
    Task<TEntity> FindAsync(ISpecification<TEntity> spec, bool throwExceptionWhenNull = true);
    Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec);
    Task<bool> AnyAsync(ISpecification<TEntity> spec);
    Task<bool> AnyAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);
    Task<TEntity> DeleteAsync(TEntity entity);
}
