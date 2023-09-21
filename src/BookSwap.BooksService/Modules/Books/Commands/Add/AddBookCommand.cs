using BookSwap.Shared.Core.Mediator;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Add
{
    public record AddBookCommand(
        Guid GenreId,
        string Title,
        string Author,
        string? Description
    ) : IRequest<Guid>, ITransactionableRequest;
}
