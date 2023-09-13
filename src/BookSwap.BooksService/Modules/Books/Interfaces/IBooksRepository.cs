using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.Shared.Data.Repositories;

namespace BookSwap.BooksService.Modules.Books.Interfaces
{
    public interface IBooksRepository : IRepository<Book>
    {
    }
}
