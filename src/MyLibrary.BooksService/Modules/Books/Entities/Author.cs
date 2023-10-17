using MyLibrary.Shared.Core.Models;

namespace MyLibrary.BooksService.Modules.Books.Entities
{
    public class Author : Entity
    {
        public string Name { get; private set; } = null!;
        public string About { get; private set; } = null!;
        public string ImageUrl { get; private set; } = null!;

        public Author(string name, string about, string imageUrl)
        {
            Name = name;
            About = about;
            ImageUrl = imageUrl;
        }

        public void Update(string name, string about, string imageUrl)
        {
            Name = name;
            About = about;
            ImageUrl = imageUrl;
        }
    }
}
