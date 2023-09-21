using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Mediator;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Add;

public record AddGenreCommandHandler(
    IGenreRepository GenreRepository, 
    IDistributedCache DistributedCache
) : IRequestHandler<AddGenreCommand, Guid>
{
    public async Task<Guid> Handle(AddGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new BookGenre(request.Name, request.Description);
        
        await GenreRepository.AddAsync(genre);

        await DistributedCache.RemoveAsync($"genres/list", cancellationToken);
        
        return genre.Id;
    }
}