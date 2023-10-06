using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data;
using BookSwap.Shared.Core.Data.Specifications;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Authors.List
{
    public class ListAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;

        public ListAuthorEndpoint(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/authors", HandleAsync)
                .WithName("ListAuthors")
                .WithTags("Authors")
                .WithDescription("Lists authors by Filter")
                .ProducesList<Author>()
                .WithOpenApi()
                .CacheOutput(p => p.SetVaryByQuery("Name").Tag("authors"));

        public async Task<IEnumerable<Author>> HandleAsync([FromQuery] string? name)
        {
            var spec = new Specification<Author>()
                .AddCriteriaIf(x => x.Name.Contains(name!), !string.IsNullOrEmpty(name))
                .AddOrderBy(x => x.Name);

            return await _authorRepository.ListAsync(spec);
        }
    }
}
