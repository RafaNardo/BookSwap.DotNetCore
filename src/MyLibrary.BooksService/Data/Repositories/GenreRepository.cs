using MyLibrary.BooksService.Modules.Books.Data.Context;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.BooksService.Modules.Books.Data.Repositories
{
    public class GenreRepository : Repository<Genre, BooksServiceDbContext>, IGenreRepository
    {
        public GenreRepository(BooksServiceDbContext context) : base(context) { }
    }
}
