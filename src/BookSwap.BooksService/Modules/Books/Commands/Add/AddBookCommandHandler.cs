using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using BookSwap.Shared.Core.Exceptions;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Add
{
    public record AddBookCommandHandler(
        IBooksRepository BooksRepository,
        IGenreRepository GenreRepository
    ) : IRequestHandler<AddBookCommand, Guid>
    {
        public async Task<Guid> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var genre = await GenreRepository.GetByIdAsync(request.GenreId, false);
            
            if (genre is null)
                throw new BadRequestException("Please provide a valid genre.");
            
            var book = new Book
            (
                title: request.Title,
                author: request.Author,
                description: request.Description,
                genre: genre
            );

            await BooksRepository.AddAsync(book);

            return book.Id;
        }
    }
}
