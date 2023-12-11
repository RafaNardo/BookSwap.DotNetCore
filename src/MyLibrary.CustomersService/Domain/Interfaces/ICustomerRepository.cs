using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.CustomersService.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
