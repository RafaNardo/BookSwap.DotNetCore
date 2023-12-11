using MyLibrary.CustomersService.Data.Context;
using MyLibrary.CustomersService.Domain.Entities;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Data.Repositories;

namespace MyLibrary.CustomersService.Data.Repositories
{
    public class CustomerRepository : Repository<Customer, CustomersServiceDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomersServiceDbContext dbContext) : base(dbContext)
        {
        }
    }
}
