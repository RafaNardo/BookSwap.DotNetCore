using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.List
{
    public class ListBookEndpoint : IEndpoint
    {
        private readonly IBooksRepository _bookRepository;

        public ListBookEndpoint(IBooksRepository bookRepository) => _bookRepository = bookRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/books", HandleAsync)
                .WithName("ListBooks")
                .WithTags("Books")
                .WithDescription("Lists books by Filter")
                .ProducesList<Book>()
                .WithValidator<ListBookRequest>()
                .WithOpenApi()
                .CacheOutput(p => p
                    .SetVaryByQuery(nameof(ListBookRequest.Author), nameof(ListBookRequest.Title))
                    .Tag("books")
                );

        public async Task<IEnumerable<Book>> HandleAsync([AsParameters] ListBookRequest request)
        {
            var spec = new Specification<Book>()
                .AddCriteriaIf(x => x.Author.Name.Contains(request.Author!), !string.IsNullOrEmpty(request.Author))
                .AddCriteriaIf(x => x.Title.Contains(request.Title!), !string.IsNullOrEmpty(request.Title))
                .AddInclude(x => x.Author)
                .AddInclude(x => x.Genre)
                .AddOrderBy(x => x.Title);

            return await _bookRepository.ListAsync(spec);
        }
    }
}
