using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.Add;

public class AddBookEndpoint : IEndpoint
{
    private readonly IBooksRepository _booksRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IOutputCacheStore _outputCacheStore;

    public AddBookEndpoint(
        IBooksRepository booksRepository, 
        IGenreRepository genreRepository,
        IOutputCacheStore outputCacheStore)
    {
        _booksRepository = booksRepository;
        _genreRepository = genreRepository;
        _outputCacheStore = outputCacheStore;
    }

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder) 
        => builder.MapPost("/api/books", HandleAsync)
            .WithName("AddBook")
            .WithTags("Books")
            .WithDescription("Adds a new book")
            .ProducesCreated()
            .ProducesUnprocessableEntity()
            .ProducesBadRequest()
            .WithTransaction()
            .WithOpenApi();

    public async Task<Guid> HandleAsync([FromBody] AddBookRequest request, CancellationToken ct)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, false);
        if (genre is null)
            throw new BadRequestException("Please provide a valid genre.");
        
        var book = new Book
        (
            title: request.Title,
            author: request.Author,
            description: request.Description,
            genre: genre
        );

        await _booksRepository.AddAsync(book);

        await _outputCacheStore.EvictByTagAsync("books", ct);

        return book.Id;
    }
}

