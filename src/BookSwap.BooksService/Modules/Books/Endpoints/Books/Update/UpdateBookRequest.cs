namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.Update;

public record UpdateBookRequest(Guid GenreId, string Title, string Author, string? Description);

