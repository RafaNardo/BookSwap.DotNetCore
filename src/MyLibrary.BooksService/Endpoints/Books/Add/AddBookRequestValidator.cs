using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.Add
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.AuthorId).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.GenreId).NotEmpty();
        }
    }
}
