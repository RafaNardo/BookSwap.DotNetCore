using BookSwap.Shared.Core.Di.Exceptions;
using BookSwap.Shared.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookSwap.Shared.Core.Di
{
    public static class AppExtensions
    {
        public static WebApplicationBuilder RegisterCoreServices(this WebApplicationBuilder builder, Assembly assembly)
        {
            builder.Services.RegisterMediatR();
            builder.Services.RegisterSwagger();
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

        public static WebApplication MigrateDatabases(this WebApplication app, Assembly assembly)
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
