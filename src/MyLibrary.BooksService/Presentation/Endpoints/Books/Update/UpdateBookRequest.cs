namespace MyLibrary.BooksService.Presentation.Endpoints.Books.Update
{
    public record UpdateBookRequest(Guid GenreId, Guid AuthorId, string Title, string? Description);
}
