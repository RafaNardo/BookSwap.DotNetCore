using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Seed
{
    public class SeedDataEndpoint : IEndpoint
    {
        private readonly IBooksRepository _booksRepository;

        public SeedDataEndpoint(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPost("/api/seed", HandleAsync)
                .WithName("SeedData")
                .WithTags(" Seed")
                .WithDescription("Add a list of books, authors and genres")
                .ProducesList<Book>()
                .ProducesBadRequest()
                .WithTransaction()
                .WithOpenApi();

        public async Task<IEnumerable<Book>> HandleAsync()
        {
            var hasAnyBook = await _booksRepository.AnyAsync();
            if (hasAnyBook)
            {
                return Enumerable.Empty<Book>();
            }

            var books = SeedData.GetBooks();

            await _booksRepository.AddRangeAsync(books);

            return books;
        }
    }
}
