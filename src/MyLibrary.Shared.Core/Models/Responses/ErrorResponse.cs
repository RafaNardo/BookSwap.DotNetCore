using FluentValidation;
using MyLibrary.Shared.Core.Exceptions;

namespace MyLibrary.Shared.Core.Models.Responses
{
    public record ErrorResponse(string Message, IEnumerable<ErrorResponseItem>? Errors = null)
    {
        public static ErrorResponse FromValidationException(ValidationException exception)
        {
            return new("Validation failed", exception.Errors.Select(e => new ErrorResponseItem(e.PropertyName, e.ErrorMessage)));
        }

        public static ErrorResponse FromBadRequestException(BadRequestException exception)
        {
            return new(exception.Message);
        }
    }
}
