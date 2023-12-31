using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Presentation.Endpoints.Authors.Delete
{
    public class DeleteAuthorEndpoint : IEndpoint
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBooksRepository _booksRepository;

        public DeleteAuthorEndpoint(IAuthorRepository authorRepository, IBooksRepository booksRepository)
        {
            _authorRepository = authorRepository;
            _booksRepository = booksRepository;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapDelete("/api/authors/{id:guid}", HandleAsync)
                .WithName("DeleteAuthor")
                .WithTags("Authors")
                .WithDescription("Delete an author by id")
                .ProducesOk()
                .ProducesNotFound()
                .ProducesBadRequest()
                .WithTransaction()
                .WithOpenApi();

        private async Task HandleAsync([FromRoute] Guid id)
        {
            var author = await _authorRepository.FindAsync(id);

            var hasAnyBook = await _booksRepository.AnyAsync(new Specification<Book>().AddCriteria(b => b.AuthorId == author.Id));
            if (hasAnyBook)
                throw new BadRequestException($"Unable to delete author '{author.Name}' because it has registered books.");

            await _authorRepository.DeleteAsync(author);
        }
    }
}
