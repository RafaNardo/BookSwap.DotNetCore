using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Add;

public record AddGenreCommandHandler(IGenreRepository GenreRepository) : IRequestHandler<AddGenreCommand, Guid>
{
    public async Task<Guid> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new BookGenre(request.Name, request.Description);
        await GenreRepository.AddAsync(genre);
        return genre.Id;
    }
}