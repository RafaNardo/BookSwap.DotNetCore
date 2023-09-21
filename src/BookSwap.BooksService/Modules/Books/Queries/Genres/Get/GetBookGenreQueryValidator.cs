using FluentValidation;

namespace BookSwap.BooksService.Modules.Books.Queries.Genres.Get;

public class GetBookGenreQueryValidator : AbstractValidator<GetBookGenreQuery>
{
    public GetBookGenreQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}