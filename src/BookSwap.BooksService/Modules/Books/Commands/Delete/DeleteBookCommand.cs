using BookSwap.Shared.Core.Data.Transactions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Delete
{
    public record DeleteBookCommand(Guid Id) : IRequest, ITransactionableRequest;
}
