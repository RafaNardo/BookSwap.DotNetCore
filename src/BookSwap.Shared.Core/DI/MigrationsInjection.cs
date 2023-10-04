using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookSwap.Shared.Core.DI;

public static class MigrationsInjection
{
        
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