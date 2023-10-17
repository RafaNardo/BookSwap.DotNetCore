using BookSwap.Shared.Core.Models;

namespace BookSwap.BooksService.Modules.Books.Entities
{
    public class Book : Entity
    {
        public string Title { get; private set; } = null!;
        public string? Description { get; private set; }
        public Genre Genre { get; private set; } = null!;
        public Author Author { get; private set; } = null!;
        public Guid AuthorId { get; private set; }
        public Guid GenreId { get; private set; }

        private Book() { }

        public Book(
            string title,
            Author author,
            Genre genre,
            string? description = null)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
        }

        public void Update(string title, Author author, Genre genre, string? description = null)
        {
            Title = title;
            Author = author;
            Description = description;
            Genre = genre;
        }
    }
}