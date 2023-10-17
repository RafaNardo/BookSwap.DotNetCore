using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Seed
{
    public class SeedDatabase : IEndpoint
    {
        private readonly IBooksRepository _booksRepository;

        public SeedDatabase(IBooksRepository booksRepository) => _booksRepository = booksRepository;

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
            var books = SeedData.GetBooks();

            var firstBookTitle = books.First().Title;

            var spec = new Specification<Book>().AddCriteria(b => b.Title.Equals(firstBookTitle));

            var foundBook = await _booksRepository.AnyAsync(spec);

            if (!foundBook)
            {
                await _booksRepository.AddRangeAsync(books);
                return books;
            }

            return Enumerable.Empty<Book>();
        }
    }
}
