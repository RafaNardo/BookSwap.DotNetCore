namespace MyLibrary.CustomersService.Domain.ValueObjects
{
    public class Address
    {
        public string State { get; }
        public string City { get; }
        public string Street { get; }
        public string Number { get; }
        public string ZipCode { get; }

        public Address(string state, string city, string street, string number, string zipCode)
        {
            State = state;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
        }
    }
}
