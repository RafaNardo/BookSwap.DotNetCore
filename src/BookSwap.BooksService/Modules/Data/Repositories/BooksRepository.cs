using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.BooksService.Modules.Data.Context;
using BookSwap.Shared.Core.Data.Repositories;

namespace BookSwap.BooksService.Modules.Data.Repositories;

public class BooksRepository : Repository<Book, BooksServiceDbContext>, IBooksRepository
{
    public BooksRepository(BooksServiceDbContext context) : base(context) { }
}