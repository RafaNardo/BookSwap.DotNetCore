using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Update;

public record UpdateGenreCommandHandler(
    IGenreRepository GenreRepository,
    IDistributedCache DistributedCache
) : IRequestHandler<UpdateGenreCommand>
{
    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await GenreRepository.GetByIdAsync(request.Id);
        
        genre.Update(request.Name, request.Description);
        
        await GenreRepository.UpdateAsync(genre);
        
        await DistributedCache.RemoveAsync($"genres/list", cancellationToken);
    }
}