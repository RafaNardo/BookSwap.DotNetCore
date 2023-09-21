using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.Shared.Core.Mediator;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.List;

public record ListBookGenreQuery(string? Name) : IRequest<IEnumerable<BookGenre>>, ICacheableRequest
{
    public string CacheKey => $"genres/list";
    public bool BypassCache => string.IsNullOrEmpty(Name); //Use cache only when list all genres
    public TimeSpan SlidingExpiration => TimeSpan.FromDays(1);
}