using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data;
using BookSwap.Shared.Core.Data.Specifications;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.List;

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

