using BookSwap.Shared.Core.Mediator;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Delete
{
    public record DeleteBookCommand(Guid Id) : IRequest, ITransactionableRequest;
}
