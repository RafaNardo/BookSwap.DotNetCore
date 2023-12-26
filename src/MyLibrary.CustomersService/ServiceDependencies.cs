using MyLibrary.CustomersService.Data.Context;
using MyLibrary.CustomersService.Data.Repositories;
using MyLibrary.CustomersService.Domain.Interfaces;
using MyLibrary.Shared.Core.Data.UoW;
using MyLibrary.Shared.Core.Modules;

namespace MyLibrary.CustomersService
{
    public class ServiceDependencies : IServiceDependencies
    {
        public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.AddRedisDistributedCache("cache");

            builder.AddSqlServerDbContext<CustomersServiceDbContext>("database");

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<CustomersServiceDbContext>>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            return builder;
        }
    }
}
