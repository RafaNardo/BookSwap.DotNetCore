using MyLibrary.CustomersService.Modules.Customers.Entities;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.CustomersService.Modules.Customers.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
