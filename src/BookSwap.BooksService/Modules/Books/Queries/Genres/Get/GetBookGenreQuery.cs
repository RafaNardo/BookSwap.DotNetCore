using BookSwap.BooksService.Modules.Books.Entities;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.Get;

public record GetBookGenreQuery(Guid Id) : IRequest<BookGenre>;