using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Extensions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.List
{
    public record ListBookRequestHandler(IBooksRepository BookRepository) : IRequestHandler<ListBookRequest, IEnumerable<Book>>
    {
        public async Task<IEnumerable<Book>> Handle(ListBookRequest request, CancellationToken cancellationToken)
        {
            return await BookRepository.ListAsync(b => b
                .WhereIf(e => e.Author.Contains(request.Author!), !string.IsNullOrEmpty(request.Author))
                .WhereIf(e => e.Title.Contains(request.Title!), !string.IsNullOrEmpty(request.Title))
            );
        }
    }
}
