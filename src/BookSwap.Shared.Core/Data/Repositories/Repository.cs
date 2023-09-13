using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Modules.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Shared.Data.Repositories;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    protected readonly TDbContext Context;

    protected Repository(TDbContext dbContext)
    {
        Context = dbContext;
    }

    public async Task<TEntity> GetByIdAsync(Guid id, bool throwExceptionWhenNull = true)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity is null)
            throw new NotFoundException();

        return entity;
    }

    public async Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> filters = null)
    {
        var query = Context.Set<TEntity>().AsNoTracking().AsQueryable();

        if (filters is not null)
            query = filters(query);

        return await query.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);

        return await Task.FromResult(entity);
    }

    public async Task<TEntity> DeleteAsync(Guid id)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity is null)
            throw new NotFoundException();
        
        Context.Set<TEntity>().Remove(entity);

        return entity;
    }
}   
