using BookSwap.Shared.Core.Mediator;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Add;

public record AddGenreCommand(string Name, string Description) : IRequest<Guid>, ITransactionableRequest;