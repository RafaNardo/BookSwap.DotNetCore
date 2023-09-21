using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Update
{
    public record UpdateBookCommandHandler(
        IBooksRepository BooksRepository,
        IGenreRepository GenreRepository
    ) : IRequestHandler<UpdateBookCommand>
    {
        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await BooksRepository.GetByIdAsync(request.Id);
            
            var newGenre = await GenreRepository.GetByIdAsync(request.GenreId);
            
            book.Update(
                title: request.Title,
                author: request.Author,
                description: request.Description,
                genre: newGenre
            );

            await BooksRepository.UpdateAsync(book);
        }
    }
}
