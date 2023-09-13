using BookSwap.Shared.Data.Transactions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Endpoints
{
    [UseTransaction]
    public record AddBookCommand(
        string Title,
        string Author,
        string? Description
    ) : IRequest<Guid>;
}
