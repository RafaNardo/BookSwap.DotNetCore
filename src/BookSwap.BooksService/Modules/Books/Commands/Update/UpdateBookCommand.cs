using MediatR;
using System.Text.Json.Serialization;
using BookSwap.Shared.Core.Data.Transactions;

namespace BookSwap.BooksService.Modules.Books.Commands.Update
{
    public record UpdateBookCommand(
        string Title,
        string Author,
        string? Description
    ) : IRequest, ITransactionableRequest
    {
        [JsonIgnore]
        public Guid Id { get; init; }
    }
}
