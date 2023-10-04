using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Shared.Core.Data.Repositories;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    protected Repository(TDbContext dbContext)
    {
        Context = dbContext;
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool throwExceptionWhenNull = true)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity is null && throwExceptionWhenNull)
            throw new NotFoundException();

        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? filters = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking().AsQueryable();

        if (filters is not null)
            query = filters(query);

        return await query.ToListAsync();
    }

    public virtual async Task<bool> AnyAsync(Func<TEntity, bool>? filters = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking().AsQueryable();

        if (filters is not null)
            return query.Any(filters);

        return await query.AnyAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);

        return await Task.FromResult(entity);
    }

    public virtual async Task<TEntity> DeleteAsync(Guid id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity is null)
            throw new NotFoundException();
        
        Context.Set<TEntity>().Remove(entity);

        return entity;
    }
    
    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);

        return await Task.FromResult(entity);
    }
}   
