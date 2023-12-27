using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.ValueObjects;

namespace MyLibrary.CustomersService.Presentation.Endpoints.Seed
{
    public class SeedData
    {
        public static List<Customer> GetCustomers()
        {
            var jackAddress = new Address("FL", "Miami", "MI. Street", "1", "12345");
            var jhonAddress = new Address("NY", "New York", "NY. Street", "2", "54321");
            var kateAddress = new Address("FL", "Orlando", "MI. Street", "3", "12543");

            // Genre objects
            var jack = new Customer("Jack", "jack@test.com", "+1234567891", jackAddress);
            var jhon = new Customer("Jhon", "jhon@test.com", "+9876543211", jhonAddress);
            var kate = new Customer("Kate", "kate@test.com", "+9876123411", kateAddress);

            return new List<Customer> { jack, jhon, kate};
        }
    }
}
