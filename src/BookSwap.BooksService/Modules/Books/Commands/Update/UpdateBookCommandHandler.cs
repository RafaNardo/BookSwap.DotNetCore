using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Update
{
    public record UpdateBookCommandHandler(
        IBooksRepository BooksRepository
    ) : IRequestHandler<UpdateBookCommand>
    {
        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await BooksRepository.GetByIdAsync(request.Id);

            book.Update(
                title: request.Title,
                author: request.Author,
                description: request.Description
            );

            await BooksRepository.UpdateAsync(book);
        }
    }
}
