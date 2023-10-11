using BookSwap.Shared.Core.Data.Specifications;
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

    public virtual async Task<TEntity> FindAsync(Guid id, bool throwNotFoundWhenNull = true)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);

        if (entity is null && throwNotFoundWhenNull)
            throw new NotFoundException();

        return entity!;
    }

    public virtual async Task<TEntity> FindAsync(ISpecification<TEntity> spec, bool throwNotFoundWhenNull = true)
    {
        var query = GetQuerableFromSpecitication(spec);

        var entity = await query.FirstOrDefaultAsync();

        if (entity is null && throwNotFoundWhenNull)
            throw new NotFoundException();

        return entity!;
    }

    public virtual async Task<IEnumerable<TEntity>> ListAsync(ISpecification<TEntity> spec)
    {
        var query = GetQuerableFromSpecitication(spec);

        return await query.ToListAsync();
    }

    public virtual async Task<bool> AnyAsync(ISpecification<TEntity> spec)
    {
        var query = GetQuerableFromSpecitication(spec);

        return await query.AnyAsync();
    }

    public virtual async Task<bool> AnyAsync(Guid id)
    {
        return await Context
            .Set<TEntity>()
            .AnyAsync(x => x.Id == id);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);

        return entity;
    }

    public virtual async Task<ICollection<TEntity>> AddRangeAsync(List<TEntity> entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);

        return entities;
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

    private IQueryable<TEntity> GetQuerableFromSpecitication(ISpecification<TEntity> spec)
    {
        var query = Context.Set<TEntity>().AsQueryable();

        if (spec.Criterias.Any())
        {
            //spec.Criterias is a list of expressions, so we need to aggregate them
            query = spec.Criterias.Aggregate(query, (current, criteria) => current.Where(criteria));
        }

        if (spec.Includes.Any())
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        if (spec.OrderBy is not null)
            query = spec.OrderBy(query);

        if (spec.Skip is not null)
            query = query.Skip(spec.Skip.Value);

        if (spec.Take is not null)
            query = query.Take(spec.Take.Value);

        return query;
    }
}   
