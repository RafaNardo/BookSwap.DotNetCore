using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.Get
{
    public class GetBookEndpoint : IEndpoint
    {
        private readonly IBooksRepository _booksRepository;

        public GetBookEndpoint(IBooksRepository booksRepository) => _booksRepository = booksRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/books/{id:guid}", HandleAsync)
                .WithName("GetBook")
                .WithTags("Books")
                .WithDescription("Gets a book by id")
                .ProducesNotFound()
                .WithOpenApi()
                .CacheOutput(p => p.Tag("books").SetVaryByRouteValue("id"));

        public async Task<Book> HandleAsync([FromRoute] Guid id)
        {
            var spec = new Specification<Book>()
                .AddCriteria(x => x.Id == id)
                .AddInclude(x => x.Author)
                .AddInclude(x => x.Genre);

            return await _booksRepository.FindAsync(spec);
        }
    }
}
