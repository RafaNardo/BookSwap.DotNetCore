using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Authors.Add
{
    public class AddAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IOutputCacheStore _outputCacheStore;

        public AddAuthorEndpoint(IAuthorRepository authorRepository, IOutputCacheStore outputCacheStore)
        {
            _authorRepository = authorRepository;
            _outputCacheStore = outputCacheStore;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPost("/api/authors", HandleAsync)
                .WithName("AddAuthor")
                .WithTags("Authors")
                .WithDescription("Adds a new author")
                .ProducesCreated()
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .WithTransaction()
                .WithValidator<AddAuthorRequest>()
                .WithOpenApi();

        public async Task<Guid> HandleAsync([FromBody] AddAuthorRequest request, CancellationToken ct)
        {
            var author = new Author(request.Name, request.About, request.ImageUrl);

            await _authorRepository.AddAsync(author);

            await _outputCacheStore.EvictByTagAsync("authors", ct);

            return author.Id;
        }
    }
}