using MyLibrary.BooksService.Modules.Books.Data.Context;
using MyLibrary.BooksService.Modules.Books.Data.Repositories;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.UoW;
using MyLibrary.Shared.Core.Modules;

namespace MyLibrary.BooksService
{
    public class ServiceDependencies : IServiceDependencies
    {
        public WebApplicationBuilder RegisterModule(WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.AddRedisDistributedCache("cache");

            builder.AddSqlServerDbContext<BooksServiceDbContext>("database");

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<BooksServiceDbContext>>();

            builder.Services.AddScoped<IBooksRepository, BooksRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();

            return builder;
        }
    }
}
