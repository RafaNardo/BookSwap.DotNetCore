using BookSwap.Shared.Core.Data.Transactions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Add
{
    public record AddBookCommand(
        string Title,
        string Author,
        string? Description
    ) : IRequest<Guid>, ITransactionableRequest;
}
