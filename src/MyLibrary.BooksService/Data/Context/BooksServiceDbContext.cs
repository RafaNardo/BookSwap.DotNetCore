using Microsoft.EntityFrameworkCore;
using MyLibrary.BooksService.Modules.Books.Entities;

namespace MyLibrary.BooksService.Modules.Books.Data.Context
{
    public class BooksServiceDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> BookGenres { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

        public BooksServiceDbContext(DbContextOptions<BooksServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
