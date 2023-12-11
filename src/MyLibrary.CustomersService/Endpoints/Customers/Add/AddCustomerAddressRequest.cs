namespace MyLibrary.CustomersService.Endpoints.Customers.Add
{
    public class AddCustomerAddressRequest
    {
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string ZipCode { get;set; } = null!;
    }
}
