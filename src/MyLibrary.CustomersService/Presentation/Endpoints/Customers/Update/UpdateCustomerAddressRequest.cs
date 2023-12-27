namespace MyLibrary.CustomersService.Presentation.Endpoints.Customers.Update
{
    public class UpdateCustomerAddressRequest
    {
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
    }
}
