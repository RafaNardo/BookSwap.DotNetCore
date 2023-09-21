using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Delete;

public record DeleteGenreCommand(Guid Id) : IRequest;