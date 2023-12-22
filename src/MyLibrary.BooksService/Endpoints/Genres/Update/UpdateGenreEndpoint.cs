using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyLibrary.BooksService.Modules.Books.Interfaces;
using MyLibrary.Shared.Core.Swagger;

namespace MyLibrary.BooksService.Modules.Books.Endpoints.Genres.Update
{
    public class UpdateGenreEndpoint : IEndpoint
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IDistributedCache _distributedCache;

        public UpdateGenreEndpoint(IGenreRepository genreRepository, IDistributedCache distributedCache)
        {
            _genreRepository = genreRepository;
            _distributedCache = distributedCache;
        }

        public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder)
            => builder.MapPut("/api/genres/{id:guid}", HandleAsync)
                .WithName("UpdateBookGenre")
                .WithDescription("Update a new book genre")
                .WithTags("Genres")
                .ProducesUnprocessableEntity()
                .ProducesBadRequest()
                .ProducesOk()
                .WithTransaction()
                .WithValidator<UpdateGenreRequest>()
                .WithOpenApi();

        public async Task HandleAsync([FromBody] UpdateGenreRequest request, [FromRoute] Guid id)
        {
            var genre = await _genreRepository.FindAsync(id);

            genre.Update(request.Name, request.Description);

            await _genreRepository.UpdateAsync(genre);

            await _distributedCache.RemoveAsync($"genres/list");
        }
    }
}
