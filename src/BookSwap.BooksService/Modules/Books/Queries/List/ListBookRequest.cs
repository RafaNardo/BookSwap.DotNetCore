using BookSwap.BooksService.Modules.Books.Entities;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.List
{
    public record ListBookRequest(string? Author, string? Title) : IRequest<IEnumerable<Book>>;
}
