using BookSwap.Shared.Core.Modules.Entities;

namespace BookSwap.BooksService.Modules.Books.Entities;

public class Book : Entity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string? Description { get; set; }
    public BookGenre Genre { get; set; }
    public Guid GenreId { get; set; }

    private Book() { }
    
    public Book(
        string title, 
        string author, 
        BookGenre genre,
        string? description = null)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Description = description;
    }

    public void Update(string title, string author, BookGenre genre, string? description = null)
    {
        Title = title;
        Author = author;
        Description = description;
        Genre = genre;
    }
}