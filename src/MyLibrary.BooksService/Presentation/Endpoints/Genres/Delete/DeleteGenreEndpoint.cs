using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Genres.Delete;

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
        var genre = await _genreRepository.FindAsync(id);

        var spec = new Specification<Book>().AddCriteria(b => b.GenreId == genre.Id);

        var hasBooks = await _booksRepository.AnyAsync(spec);

        if (hasBooks)
            throw new BadRequestException("Cannot delete genre with books");

        await _genreRepository.DeleteAsync(genre);
    }
}
