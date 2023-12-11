namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.Add
{
    public record AddBookRequest(Guid GenreId, Guid AuthorId, string Title, string? Description);
}
