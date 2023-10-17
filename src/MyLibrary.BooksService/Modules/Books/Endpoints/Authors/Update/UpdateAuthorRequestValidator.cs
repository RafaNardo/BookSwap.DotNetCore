using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Authors.Update
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
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
