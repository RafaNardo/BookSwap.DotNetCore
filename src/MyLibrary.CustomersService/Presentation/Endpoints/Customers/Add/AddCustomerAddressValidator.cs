using FluentValidation;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Add
{
    public class AddCustomerAddressValidator : AbstractValidator<AddCustomerAddressRequest>
    {
        public AddCustomerAddressValidator()
        {
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
        }
    }
}
