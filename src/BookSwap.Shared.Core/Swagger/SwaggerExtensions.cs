using BookSwap.Shared.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookSwap.Shared.Core.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static WebApplication UseSwaggerUI(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                SwaggerUIBuilderExtensions.UseSwaggerUI(app);
            }

            return app;
        }

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
