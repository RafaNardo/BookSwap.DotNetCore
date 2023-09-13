using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Commands.Delete
{
    public record DeleteBookCommandHandler(IBooksRepository BooksRepository) : IRequestHandler<DeleteBookCommand>
    {
        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await BooksRepository.DeleteAsync(request.Id);
        }
    }
}
