using MyLibrary.BooksService.Modules.Books.Data.Context;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.BooksService.Modules.Books.Data.Repositories
{
    public class AuthorRepository : Repository<Author, BooksServiceDbContext>, IAuthorRepository
    {
        public AuthorRepository(BooksServiceDbContext context) : base(context) { }
    }
}
