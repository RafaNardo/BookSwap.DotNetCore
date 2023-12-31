using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Authors.Get
{
    public class GetAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorEndpoint(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/authors/{id:guid}", HandleAsync)
                .WithName("GetAuthor")
                .WithTags("Authors")
                .WithDescription("Get author by id")
                .Produces<Author>()
                .ProducesNotFound()
                .WithOpenApi()
                .CacheOutput(p => p.SetVaryByQuery("Id").Tag("authors"));

        public async Task<Author> HandleAsync([FromRoute] Guid id)
        {
            return await _authorRepository.FindAsync(id);
        }
    }
}
