using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Exceptions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Delete;

public record DeleteGenreCommandHandler(IBooksRepository BooksRepository, IGenreRepository GenreRepository) : IRequestHandler<DeleteGenreCommand>
{
    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await GenreRepository.GetByIdAsync(request.Id);

        var hasBooks = await BooksRepository.AnyAsync(b => b.GenreId == genre.Id);
        
        if (hasBooks)
            throw new BadRequestException("Cannot delete genre with books");
        
        await GenreRepository.DeleteAsync(genre);
    }
}