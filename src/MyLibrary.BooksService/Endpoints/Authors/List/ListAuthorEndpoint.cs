using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Authors.List
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
