using Microsoft.AspNetCore.Mvc;
using MyLibrary.BooksService.Modules.Books.Entities;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Data.Specifications;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Genres.List
{
    public record ListBookGenreEndpoint : IEndpoint
    {
        private readonly IGenreRepository _genreRepository;

        public ListBookGenreEndpoint(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapGet("/api/genres", HandleAsync)
                .WithName("ListBookGenres")
                .WithTags("Genres")
                .WithDescription("Lists books genres by Filter")
                .ProducesList<Book>()
                .WithOpenApi();

        public async Task<IEnumerable<Genre>> HandleAsync([FromQuery] string? name)
        {
            var spec = new Specification<Genre>()
                .AddCriteriaIf(x => x.Name.Contains(name!), !string.IsNullOrEmpty(name))
                .AddOrderBy(x => x.Name);

            return await _genreRepository.ListAsync(spec);
        }
    }
}
