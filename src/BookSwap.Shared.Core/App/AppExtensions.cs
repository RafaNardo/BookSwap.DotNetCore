using System.Reflection;
using BookSwap.Shared.Core.Cache;
using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Mediator;
using BookSwap.Shared.Core.Modules;
using BookSwap.Shared.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookSwap.Shared.Core.App
{
    public static class AppExtensions
    {
        public static WebApplicationBuilder RegisterCoreServices(this WebApplicationBuilder builder, Assembly assembly)
        {
            builder.Services.RegisterMediatR();
            builder.Services.RegisterSwagger();
            builder.Services.AddStackExchangeRedis(builder.Configuration, instance: assembly.GetName().Name!);
            builder.Services.RegisterModules(assembly, builder.Configuration);

            return builder;
        }

        public static WebApplication UseCoreServices(this WebApplication app, Assembly assembly)
        {
            app.UseCustomExceptionHandler();
            app.UseSwaggerUI();
            app.UseModules();
            app.MigrateDatabases(assembly);

            return app;
        }

        private static WebApplication MigrateDatabases(this WebApplication app, Assembly assembly)
        {
            var dbContextTypes = assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(DbContext)))
                .ToList();

            foreach (var dbContextType in dbContextTypes)
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;
                dbContext?.Database?.Migrate();
            }

            return app;
        }
    }
}
