using BookSwap.BooksService.Modules.Books.Entities;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.List;

public record ListBookGenreQuery(string? Name) : IRequest<IEnumerable<BookGenre>>;