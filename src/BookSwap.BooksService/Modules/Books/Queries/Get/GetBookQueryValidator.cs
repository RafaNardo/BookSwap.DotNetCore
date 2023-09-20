using FluentValidation;

namespace BookSwap.BooksService.Modules.Books.Queries.Get;

public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
{
    public GetBookQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}