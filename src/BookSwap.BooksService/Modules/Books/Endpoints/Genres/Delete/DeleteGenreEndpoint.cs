﻿using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Genres.Delete;

public record DeleteGenreEndpoint : IEndpoint
{
    private readonly IBooksRepository _booksRepository;
    private readonly IGenreRepository _genreRepository;
    
    public DeleteGenreEndpoint(IBooksRepository booksRepository, IGenreRepository genreRepository)
    {
        _booksRepository = booksRepository;
        _genreRepository = genreRepository;
    }

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
        => builder.MapDelete("/api/genres/{id:guid}", HandleAsync)
            .WithName("DeleteBookGenre")
            .WithTags("Genres")
            .WithDescription("Delete a book genre by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithTransaction()
            .WithOpenApi();
    
    public async Task HandleAsync([FromRoute] Guid id)
    {
        var genre = await _genreRepository.Find(id);

        var hasBooks = await _booksRepository.AnyAsync(b => b.GenreId == genre.Id);
        
        if (hasBooks)
            throw new BadRequestException("Cannot delete genre with books");
        
        await _genreRepository.DeleteAsync(genre);
    }
}