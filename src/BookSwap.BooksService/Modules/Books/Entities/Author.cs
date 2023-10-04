using BookSwap.Shared.Core.Models;

namespace BookSwap.BooksService.Modules.Books.Entities
{
    public class Author : Entity
    {
        public string Name { get; private set; } = null!;
        public string About { get; private set; } = null!;
        public string ImageUrl { get; private set; } = null!;
        public List<Book> Books { get; private set; } = new();

        public Author(string name, string about, string imageUrl)
        {
            Name = name;
            About = about;
            ImageUrl = imageUrl;
        }
    }
}
