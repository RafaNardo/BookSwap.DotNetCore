using MyLibrary.CustomersService.Domain.ValueObjects;
using MyLibrary.Shared.Core.Models;

namespace MyLibrary.CustomersService.Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public Phone Phone { get; private set; } = null!;
        public Address Address { get; private set; } = null!;

        private Customer() { }

        public Customer(string name, string email, Address address)
        {
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
