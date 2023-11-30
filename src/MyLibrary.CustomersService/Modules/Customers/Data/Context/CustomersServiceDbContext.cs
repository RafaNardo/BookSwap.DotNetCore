using Microsoft.EntityFrameworkCore;
using MyLibrary.CustomersService.Modules.Customers.Entities;

namespace MyLibrary.CustomersService.Modules.Customers.Data.Context
{
    public class CustomersServiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;

        public CustomersServiceDbContext(DbContextOptions<CustomersServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
