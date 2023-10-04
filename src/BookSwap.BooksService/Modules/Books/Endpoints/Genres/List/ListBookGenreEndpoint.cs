using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Genres.List;

public record ListBookGenreEndpoint : IEndpoint
{
    private readonly IGenreRepository _genreRepository;
    
    public ListBookGenreEndpoint(IGenreRepository genreRepository) => _genreRepository = genreRepository;

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder.MapGet("/api/genres", HandleAsync)
            .WithName("ListBookGenres")
            .WithTags("Genres")
            .WithDescription("Lists books genres by Filter")
            .ProducesList<Book>()
            .WithOpenApi();

    public async Task<IEnumerable<Genre>> HandleAsync([FromQuery] string? name)
    {
        return await _genreRepository.ListAsync(q => 
            q.WhereIf(g => g.Name.Contains(name!), !string.IsNullOrEmpty(name)) 
        );
    }

}