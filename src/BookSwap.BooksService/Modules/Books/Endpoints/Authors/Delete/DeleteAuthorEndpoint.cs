using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Authors.Delete
{
    public class DeleteAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorEndpoint(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapDelete("/api/authors/{id:guid}", HandleAsync)
                .WithName("DeleteAuthor")
                .WithTags("Authors")
                .WithDescription("Delete an author by id")
                .ProducesOk()
                .ProducesNotFound()
                .ProducesBadRequest()
                .WithTransaction()
                .WithOpenApi();

        private async Task HandleAsync([FromRoute] Guid id)
        {
            var book = await _authorRepository.FindAsync(id);

            await _authorRepository.DeleteAsync(book);
        }
    }
}
