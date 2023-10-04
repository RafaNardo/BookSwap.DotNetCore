using BookSwap.Shared.Core.Models;

namespace BookSwap.BooksService.Modules.Books.Entities;

public class Book : Entity
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string? Description { get; set; }
    public Genre Genre { get; set; } = null!;
    public Guid GenreId { get; set; }

    private Book() { }
    
    public Book(
        string title, 
        string author, 
        Genre genre,
        string? description = null)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Description = description;
    }

    public void Update(string title, string author, Genre genre, string? description = null)
    {
        Title = title;
        Author = author;
        Description = description;
        Genre = genre;
    }
}