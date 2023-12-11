using MyLibrary.BooksService.Modules.Books.Data.Context;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.BooksService.Modules.Books.Data.Repositories
{
    public class BooksRepository : Repository<Book, BooksServiceDbContext>, IBooksRepository
    {
        public BooksRepository(BooksServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
