using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Shared.Core.Modules;
using System.Reflection;

namespace MyLibrary.Shared.Core.DI
{
    public static class ServiceDependenciesInjection
    {
        public static WebApplicationBuilder AddServiceDependencies(this WebApplicationBuilder builder, Assembly assembly)
        {
            var modules = assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IServiceDependencies)))
                .Select(Activator.CreateInstance)
                .Cast<IServiceDependencies>(); ;

            foreach (var module in modules)
                module.RegisterModule(builder, builder.Configuration);

            builder.Services.AddSwagger();

            builder.Services.AddValidatorsFromAssembly(assembly);

            builder.Services.AddOutputCache(options => { options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(5); });

            builder.Services.AddEndpoints();

            return builder;
        }
    }
}
