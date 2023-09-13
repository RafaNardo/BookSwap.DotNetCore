using BookSwap.BooksService.Modules.Books.Data.Context;
using BookSwap.BooksService.Modules.Books.Data.Repositories;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Modules;
using BookSwap.Shared.Data.UoW;
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
