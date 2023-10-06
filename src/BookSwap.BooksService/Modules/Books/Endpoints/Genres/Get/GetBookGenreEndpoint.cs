using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Genres.Get;

public record GetBookGenreEndpoint : IEndpoint
{
    private readonly IGenreRepository _genreRepository;
    
    public GetBookGenreEndpoint(IGenreRepository genreRepository) => _genreRepository = genreRepository;
    
    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder.MapGet("/api/genres/{id:guid}", HandleAsync)
            .WithName("GetBookGenre")
            .WithTags("Genres")
            .WithDescription("Get a book genre by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithOpenApi();

    public async Task<Genre> HandleAsync([FromRoute] Guid id)
    {
        return await _genreRepository.FindAsync(id);
    }
}