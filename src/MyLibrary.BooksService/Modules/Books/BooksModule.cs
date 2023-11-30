using MyLibrary.BooksService.Modules.Books.Data.Context;
using MyLibrary.BooksService.Modules.Books.Data.Repositories;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.UoW;
using MyLibrary.Shared.Core.Modules;
using Microsoft.EntityFrameworkCore;

namespace MyLibrary.BooksService.Modules.Books
{
    public class BooksModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<BooksServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BooksServiceDb"));
            });

            builder.AddScoped<IUnitOfWork, UnitOfWork<BooksServiceDbContext>>();

            builder.AddScoped<IBooksRepository, BooksRepository>();
            builder.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.AddScoped<IGenreRepository, GenreRepository>();

            return builder;
        }
    }
}
