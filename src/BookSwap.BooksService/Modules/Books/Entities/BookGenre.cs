using BookSwap.Shared.Core.Modules.Entities;

namespace BookSwap.BooksService.Modules.Books.Entities;

public class BookGenre : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    private BookGenre() { }
    public BookGenre(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}