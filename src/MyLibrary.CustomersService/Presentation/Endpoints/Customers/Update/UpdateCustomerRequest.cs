namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Update
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public UpdateCustomerAddressRequest Address { get; set; } = null!;
    }
}
