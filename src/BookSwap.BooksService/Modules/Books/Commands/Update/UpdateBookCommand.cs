using BookSwap.BooksService.Modules.Books.Endpoints;
using BookSwap.Shared.Data.Transactions;
using MediatR;
using System.Text.Json.Serialization;

namespace BookSwap.BooksService.Modules.Books.Commands.Update
{
    [UseTransaction]
    public record UpdateBookCommand(
        string Title,
        string Author,
        string? Description
    ) : IRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}
