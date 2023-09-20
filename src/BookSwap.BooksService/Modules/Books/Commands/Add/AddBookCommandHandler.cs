using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Add
{
    public record AddBookCommandHandler(IBooksRepository BooksRepository) : IRequestHandler<AddBookCommand, Guid>
    {
        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            (
                title: request.Title,
                author: request.Author,
                description: request.Description
            );

            await BooksRepository.AddAsync(book);

            return book.Id;
        }
    }
}
