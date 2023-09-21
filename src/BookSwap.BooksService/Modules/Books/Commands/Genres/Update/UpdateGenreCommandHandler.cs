using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Update;

public record UpdateGenreCommandHandler(IGenreRepository GenreRepository) : IRequestHandler<UpdateGenreCommand>
{
    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await GenreRepository.GetByIdAsync(request.Id);
        genre.Update(request.Name, request.Description);
        await GenreRepository.UpdateAsync(genre);
    }
}