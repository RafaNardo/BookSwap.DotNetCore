using BookSwap.Shared.Core.Mediator;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace BookSwap.BooksService.Modules.Books.Commands.Update
{
    public record UpdateBookCommand(
        Guid GenreId,
        string Title,
        string Author,
        string? Description
    ) : IRequest, ITransactionableRequest
    {
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; init; }
    }
}
