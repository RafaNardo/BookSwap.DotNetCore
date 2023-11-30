using MyLibrary.CustomersService.Modules.Customers.Data.Context;
using MyLibrary.CustomersService.Modules.Customers.Entities;
using MyLibrary.CustomersService.Modules.Customers.Interfaces;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.CustomersService.Modules.Customers.Data.Repositories
{
    public class CustomerRepository : Repository<Customer, CustomersServiceDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomersServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
