using BookSwap.Shared.Core.Mediator;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Update;

public record UpdateGenreCommand(string Name, string Description) : IRequest, ITransactionableRequest
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; init; }
}