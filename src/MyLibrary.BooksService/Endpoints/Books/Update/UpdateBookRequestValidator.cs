using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.Update
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.GenreId).NotEmpty();
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        }
    }
}
