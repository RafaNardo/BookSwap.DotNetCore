using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookSwap.Shared.Core.DI
{
    public static class SwaggerInjection
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => c.EnableAnnotations());

            return services;
        }

        public static WebApplication ConfigureSwagger(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                SwaggerUIBuilderExtensions.UseSwaggerUI(app);
            }

            return app;
        }
    }
}