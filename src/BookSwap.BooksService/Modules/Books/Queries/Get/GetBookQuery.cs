using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.Shared.Core.Mediator;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Get;

public record GetBookQuery(Guid Id, bool BypassCache = false) : IRequest<Book>, ICacheableRequest
{
    public string CacheKey => $"/Books/{Id}";
    public TimeSpan SlidingExpiration => TimeSpan.FromMinutes(30);
}