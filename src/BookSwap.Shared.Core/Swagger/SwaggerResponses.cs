using BookSwap.Shared.Core.Models.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BookSwap.Shared.Core.Swagger
{
    public static class SwaggerResponses
    {
        public static RouteHandlerBuilder ProducesList<TResponse>(this RouteHandlerBuilder builder)
            => builder.Produces<IEnumerable<TResponse>>();

        public static RouteHandlerBuilder ProducesUnprocessableEntity(this RouteHandlerBuilder builder)
            => builder.Produces<ErrorResponse>(StatusCodes.Status422UnprocessableEntity);

        public static RouteHandlerBuilder ProducesBadRequest(this RouteHandlerBuilder builder)
            => builder.Produces<ErrorResponse>(StatusCodes.Status400BadRequest);

        public static RouteHandlerBuilder ProducesNotFound(this RouteHandlerBuilder builder)
            => builder.Produces(StatusCodes.Status404NotFound);

        public static RouteHandlerBuilder ProducesOk(this RouteHandlerBuilder builder)
            => builder.Produces(StatusCodes.Status200OK);

        public static RouteHandlerBuilder ProducesCreated(this RouteHandlerBuilder builder)
            => builder.Produces<CreatedResponse>(StatusCodes.Status201Created);
    }
}
