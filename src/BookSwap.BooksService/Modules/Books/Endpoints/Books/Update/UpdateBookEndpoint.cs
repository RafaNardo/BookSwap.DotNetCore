using BookSwap.BooksService.Modules.Books.Interfaces;
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
        
        public UpdateBookEndpoint(
            IBooksRepository booksRepository, 
            IGenreRepository genreRepository,
            IOutputCacheStore outputCacheStore)
        {
            _outputCacheStore = outputCacheStore;
            _booksRepository = booksRepository;
            _genreRepository = genreRepository;
        }
            
        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder) 
            => builder.MapPut("/api/books/{id:guid}", HandleAsync)
                .WithName("UpdateBook")
                .WithTags("Books")
                .WithDescription("Update a new book")
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .ProducesOk()
                .WithOpenApi();

        public async Task HandleAsync(
            [FromBody] UpdateBookRequest request, 
            [FromRoute] Guid id,
            CancellationToken ct
        )
        {
            var book = await _booksRepository.GetByIdAsync(id);
           
            var newGenre = await _genreRepository.GetByIdAsync(request.GenreId, false);
            if (newGenre is null)
                throw new BadRequestException("Please provide a valid genre.");

            book.Update(
                 title: request.Title,
                 author: request.Author,
                 description: request.Description,
                genre: newGenre
            );

            await _booksRepository.UpdateAsync(book);

            await _outputCacheStore.EvictByTagAsync("books", ct);
        }
    }
}
