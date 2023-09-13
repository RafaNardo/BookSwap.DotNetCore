using BookSwap.BooksService.Modules.Books.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.BooksService.Modules.Books.Data.Context;

public class BooksServiceDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    public BooksServiceDbContext(DbContextOptions<BooksServiceDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}