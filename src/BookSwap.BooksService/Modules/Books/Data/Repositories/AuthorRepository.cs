using BookSwap.BooksService.Modules.Books.Data.Context;
using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data.Repositories;

namespace BookSwap.BooksService.Modules.Books.Data.Repositories;

public class AuthorRepository : Repository<Author, BooksServiceDbContext>, IAuthorRepository
{
    public AuthorRepository(BooksServiceDbContext context) : base(context) { }
}