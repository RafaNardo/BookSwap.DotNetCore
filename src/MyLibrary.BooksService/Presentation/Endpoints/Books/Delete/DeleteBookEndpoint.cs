using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Books.Delete
{
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
}
