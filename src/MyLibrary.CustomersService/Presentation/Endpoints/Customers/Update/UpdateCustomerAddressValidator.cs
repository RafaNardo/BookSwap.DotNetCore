using FluentValidation;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Update
{
    public class UpdateCustomerAddressValidator : AbstractValidator<UpdateCustomerAddressRequest>
    {
        public UpdateCustomerAddressValidator()
        {
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
        }
    }
}
