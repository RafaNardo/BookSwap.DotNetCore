using FluentValidation;
using MyLibrary.CustomersService.Domain.ValueObjects;

namespace MyLibrary.CustomersService.Endpoints.Customers.Add
{
    public class AddCustomerValidator  : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().Must(Phone.IsValid).WithMessage("Invalid phone number");
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
