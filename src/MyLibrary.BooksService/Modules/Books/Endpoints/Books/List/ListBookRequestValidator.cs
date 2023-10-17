using FluentValidation;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.List
{
    public class ListBookRequestValidator : AbstractValidator<ListBookRequest>
    {
        public ListBookRequestValidator()
        {
            RuleFor(x => x).Must(HaveAtLeastOneFilter).WithMessage("At least one filter must be provided");
        }

        private bool HaveAtLeastOneFilter(ListBookRequest request)
        {
            return !string.IsNullOrWhiteSpace(request.Author) || !string.IsNullOrWhiteSpace(request.Title);
        }
    }
}
