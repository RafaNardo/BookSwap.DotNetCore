using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.BooksService.Modules.Data.Context;
using BookSwap.Shared.Core.Data.Repositories;

namespace BookSwap.BooksService.Modules.Data.Repositories;

public class GenreRepository : Repository<BookGenre, BooksServiceDbContext>, IGenreRepository
{
    public GenreRepository(BooksServiceDbContext context) : base(context) { }
}