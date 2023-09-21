using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.Get;

public record GetBookGenreQueryHandler(IGenreRepository GenreRepository) : IRequestHandler<GetBookGenreQuery, BookGenre>
{
    public async Task<BookGenre> Handle(GetBookGenreQuery request, CancellationToken cancellationToken)
    {
        return await GenreRepository.GetByIdAsync(request.Id);
    }
}