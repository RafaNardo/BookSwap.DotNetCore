using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.EndpointFilters;
using BookSwap.Shared.Core.Endpoints;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.Delete;

public class DeleteBookEndpoint : IEndpoint
{
    private readonly IBooksRepository _booksRepository;

    public DeleteBookEndpoint(IBooksRepository booksRepository) => _booksRepository = booksRepository;

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder) 
        => builder.MapDelete("/api/books/{id:guid}", HandleAsync)
            .WithName("DeleteBook")
            .WithTags("Books")
            .WithDescription("Delete a book by id")
            .ProducesOk()
            .ProducesNotFound()
            .ProducesBadRequest()
            .WithTransaction()
            .WithOpenApi();

    private async Task HandleAsync([FromRoute] Guid id)
    {
        var book = await _booksRepository.FindAsync(id);
      
        await _booksRepository.DeleteAsync(book);
    }
}
