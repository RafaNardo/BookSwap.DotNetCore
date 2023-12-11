using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Authors.Add
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.About)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
