namespace BookSwap.BooksService.Modules.Books.Endpoints.Books.Add
{
    public record AddBookRequest(Guid GenreId, string Title, string Author, string? Description);
}
