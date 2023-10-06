using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.Update
{
    public class UpdateBookEndpoint : IEndpoint
    {
        private readonly IOutputCacheStore _outputCacheStore;
        private readonly IBooksRepository _booksRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;

        public UpdateBookEndpoint(
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
            => builder.MapPut("/api/books/{id:guid}", HandleAsync)
                .WithName("UpdateBook")
                .WithTags("Books")
                .WithDescription("Update a new book")
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .ProducesOk()
                .WithTransaction()
                .WithValidator<UpdateBookRequest>()
                .WithOpenApi();

        public async Task HandleAsync(
            [FromBody] UpdateBookRequest request, 
            [FromRoute] Guid id,
            CancellationToken ct
        )
        {
            var book = await _booksRepository.Find(id);
           
            var genre = await _genreRepository.Find(request.GenreId, false);
            if (genre is null)
                throw new BadRequestException("Please provide a valid genre.");

            var author = await _authorRepository.Find(request.AuthorId, false);
            if (author is null)
                throw new BadRequestException("Please provide a valid author.");

            book.Update(
                 title: request.Title,
                 author: author,
                 description: request.Description,
                genre: genre
            );

            await _booksRepository.UpdateAsync(book);

            await _outputCacheStore.EvictByTagAsync("books", ct);
        }
    }
}
