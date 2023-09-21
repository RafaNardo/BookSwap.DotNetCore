using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Extensions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.List;

public record ListBookGenreQueryHandler(IGenreRepository GenreRepository) : IRequestHandler<ListBookGenreQuery, IEnumerable<BookGenre>>
{
    public async Task<IEnumerable<BookGenre>> Handle(ListBookGenreQuery request, CancellationToken cancellationToken)
    {
        return await GenreRepository.ListAsync(q => 
            q.WhereIf(g => g.Name.Contains(request.Name!), !string.IsNullOrEmpty(request.Name)) 
        );
    }
}