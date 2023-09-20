using BookSwap.BooksService.Modules.Books.Entities;
using BookSwap.BooksService.Modules.Books.Interfaces;
using MediatR;

namespace BookSwap.BooksService.Modules.Books.Queries.Get;

public record GetBookQueryHandler(IBooksRepository BooksRepository) : IRequestHandler<GetBookQuery, Book>
{
    public async Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken) 
        => await BooksRepository.GetByIdAsync(request.Id);
}