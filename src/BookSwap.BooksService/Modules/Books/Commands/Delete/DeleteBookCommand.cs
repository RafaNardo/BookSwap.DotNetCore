using BookSwap.Shared.Data.Transactions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Delete
{
    [UseTransaction]
    public record DeleteBookCommand(Guid Id) : IRequest;
}
