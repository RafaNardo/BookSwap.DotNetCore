using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Genres.Add;

public class AddGenreRequestValidator : AbstractValidator<AddGenreRequest>
{
    public AddGenreRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
    }
}
