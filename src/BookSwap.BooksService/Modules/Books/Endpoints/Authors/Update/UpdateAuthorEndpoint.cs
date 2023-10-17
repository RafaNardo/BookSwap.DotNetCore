using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Authors.Update
{
    public class UpdateAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IOutputCacheStore _outputCacheStore;

        public UpdateAuthorEndpoint(IAuthorRepository authorRepository, IOutputCacheStore outputCacheStore)
        {
            _authorRepository = authorRepository;
            _outputCacheStore = outputCacheStore;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPut("/api/authors/{id:guid}", HandleAsync)
                .WithName("UpdateAuthor")
                .WithTags("Authors")
                .WithDescription("Update an existing author")
                .ProducesOk()
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .WithTransaction()
                .WithOpenApi();

        public async Task<Guid> HandleAsync(
            [FromBody] UpdateAuthorRequest request,
            [FromRoute] Guid id,
            CancellationToken ct)
        {
            var author = await _authorRepository.FindAsync(id);

            author.Update(request.Name, request.About, request.ImageUrl);

            await _authorRepository.UpdateAsync(author);

            await _outputCacheStore.EvictByTagAsync("authors", ct);

            return author.Id;
        }
    }
}