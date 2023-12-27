using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Books.Add
{
    public class AddBookEndpoint : IEndpoint
    {
        private readonly IOutputCacheStore _outputCacheStore;
        private readonly IBooksRepository _booksRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public AddBookEndpoint(
            IBooksRepository booksRepository,
            IGenreRepository genreRepository,
            IAuthorRepository authorRepository,
            IOutputCacheStore outputCacheStore)
        {
            _booksRepository = booksRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
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
                .WithValidator<AddBookRequest>()
                .WithTransaction()
                .WithOpenApi();

        public async Task<Guid> HandleAsync([FromBody] AddBookRequest request, CancellationToken ct)
        {
            var genre = await _genreRepository.FindAsync(request.GenreId, false);
            if (genre is null)
                throw new BadRequestException("Please provide a valid genre.");

            var author = await _authorRepository.FindAsync(request.AuthorId, false);
            if (author is null)
                throw new BadRequestException("Please provide a valid author.");

            var book = new Book
            (
                title: request.Title,
                author: author,
                description: request.Description,
                genre: genre
            );

            await _booksRepository.AddAsync(book);

            await _outputCacheStore.EvictByTagAsync("books", ct);

            return book.Id;
        }
    }
}
