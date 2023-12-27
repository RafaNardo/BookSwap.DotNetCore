using Microsoft.Extensions.Caching.Distributed;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Genres.Add;

public class AddGenreEndpoint : IEndpoint
{
    private readonly IGenreRepository _genreRepository;
    private readonly IDistributedCache _distributedCache;

    public AddGenreEndpoint(IGenreRepository genreRepository, IDistributedCache distributedCache)
    {
        _genreRepository = genreRepository;
        _distributedCache = distributedCache;
    }

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder.MapPost("/api/genres", HandleAsync)
            .WithName("AddBookGenre")
            .WithDescription("Adds a new book genre")
            .WithTags("Genres")
            .ProducesCreated()
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .WithTransaction()
            .WithValidator<AddGenreRequest>()
            .WithOpenApi();

    public async Task<Guid> HandleAsync(AddGenreRequest request, CancellationToken cancellationToken)
    {
        var genre = new Genre(request.Name, request.Description);

        await _genreRepository.AddAsync(genre);

        await _distributedCache.RemoveAsync($"genres/list", cancellationToken);

        return genre.Id;
    }
}
