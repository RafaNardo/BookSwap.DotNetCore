using BookSwap.BooksService.Modules.Books.Data.Context;
using BookSwap.BooksService.Modules.Books.Data.Repositories;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Data.UoW;
using BookSwap.Shared.Core.Modules;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.BooksService.Modules.Books
{
    public class BooksModule : IModule
    {
        public IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddDbContext<BooksServiceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BooksServiceDb"));
            });

            builder.AddTransient<IUnitOfWork, UnitOfWork<BooksServiceDbContext>>();

            builder.AddScoped<IBooksRepository, BooksRepository>();
            builder.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.AddScoped<IGenreRepository, GenreRepository>();

            return builder;
        }
    }
}