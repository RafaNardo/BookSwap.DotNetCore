using Microsoft.EntityFrameworkCore;
using MyLibrary.CustomersService.Data.Context;
using MyLibrary.CustomersService.Data.Repositories;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Data.UoW;
using MyLibrary.Shared.Core.Modules;

namespace MyLibrary.CustomersService.Domain
{
    public class CustomersModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<CustomersServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CurtomerServiceDb"));
            });

            builder.AddScoped<IUnitOfWork, UnitOfWork<CustomersServiceDbContext>>();

            builder.AddScoped<ICustomerRepository, CustomerRepository>();

            return builder;
        }
    }
}
