using FluentValidation;

namespace BookSwap.BooksService.Modules.Books.Commands.Genres.Delete;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}