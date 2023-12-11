using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.BooksService.Modules.Books.Interfaces
{
    public interface IBooksRepository : IRepository<Book>
    {
    }
}
