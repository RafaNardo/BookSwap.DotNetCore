using BookSwap.Shared.Core.Modules.Entities;

namespace BookSwap.BooksService.Modules.Books.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Description { get; set; }

        public Book(string title, string author, string? description = null)
        {
            Title = title;
            Author = author;
            Description = description;
        }

        public void Update(string title, string author, string? description = null)
        {
            Title = title;
            Author = author;
            Description = description;
        }
    }
}
