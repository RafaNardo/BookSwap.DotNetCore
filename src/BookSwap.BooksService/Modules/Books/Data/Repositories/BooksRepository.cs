using BookSwap.BooksService.Modules.Books.Data.Context;
using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data.Repositories;

namespace BookSwap.BooksService.Modules.Books.Data.Repositories
{
    public class BooksRepository : Repository<Book, BooksServiceDbContext>, IBooksRepository
    {
        public BooksRepository(BooksServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}