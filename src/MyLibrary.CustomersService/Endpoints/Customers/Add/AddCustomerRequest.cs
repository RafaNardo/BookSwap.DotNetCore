namespace MyLibrary.CustomersService.Endpoints.Customers.Add
{
    public class AddCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public AddCustomerAddressRequest Address { get; set; } = null!;
    }
}
