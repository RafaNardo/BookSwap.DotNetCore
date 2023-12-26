using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Data.UoW;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Seed
{
    public class SeedDatabaseEndpoint : IEndpoint
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeedDatabaseEndpoint(IBooksRepository booksRepository, IUnitOfWork unitOfWork)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
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

            var books = SeedDatabaseData.GetBooks();

            await _booksRepository.AddRangeAsync(books);

            return books;
        }
    }
}
