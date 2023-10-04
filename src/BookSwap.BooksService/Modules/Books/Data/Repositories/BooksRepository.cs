using BookSwap.BooksService.Modules.Books.Data.Context;
using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data.Repositories;
using BookSwap.Shared.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.BooksService.Modules.Books.Data.Repositories;

public class BooksRepository : Repository<Book, BooksServiceDbContext>, IBooksRepository
{
    public BooksRepository(BooksServiceDbContext context) : base(context) { }

    public override async Task<Book> GetByIdAsync(Guid id, bool throwExceptionWhenNull = true)
    {
        var entity = await Context.Books
            .Include(x => x.Genre)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            throw new NotFoundException();

        return entity;
    }

    public override async Task<IEnumerable<Book>> ListAsync(Func<IQueryable<Book>, IQueryable<Book>>? filters = null)
    {
        var query = Context.Books
            .Include(x => x.Genre)
            .AsQueryable();

        if (filters != null)
            query = filters(query);

        return await query.AsNoTracking().ToListAsync();
    }
}