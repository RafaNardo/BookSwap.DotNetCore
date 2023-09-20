using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.BooksService.Modules.Data.Context;
using BookSwap.BooksService.Modules.Data.Repositories;
using BookSwap.Shared.Core.Data.UoW;
using BookSwap.Shared.Core.Modules;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.BooksService.Modules.Data
{
    public class DataModule : IModule
    {
        public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;

        public IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BooksServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BooksServiceDb"));
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork<BooksServiceDbContext>>();
            
            services.AddScoped<IBooksRepository, BooksRepository>();
            
            return services;
        }
    }
}
