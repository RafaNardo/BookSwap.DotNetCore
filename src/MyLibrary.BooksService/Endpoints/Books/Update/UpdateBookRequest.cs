namespace MyLibrary.BooksService.Modules.Books.Endpoints.Books.Update
{
    public record UpdateBookRequest(Guid GenreId, Guid AuthorId, string Title, string? Description);
}
