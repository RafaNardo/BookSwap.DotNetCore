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

        internal Customer(string name, string email, Phone phone, Address address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        internal void Update(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

        internal void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}
