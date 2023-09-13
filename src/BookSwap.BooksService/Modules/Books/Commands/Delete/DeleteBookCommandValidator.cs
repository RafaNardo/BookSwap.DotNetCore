using BookSwap.BooksService.Modules.Books.Interfaces;
using FluentValidation;

namespace BookSwap.BooksService.Modules.Books.Commands.Delete
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator(IBooksRepository BooksRepository)
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
