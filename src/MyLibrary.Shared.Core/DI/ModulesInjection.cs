using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Shared.Core.Modules;
using System.Reflection;

namespace MyLibrary.Shared.Core.DI
{
    public static class ModulesInjection
    {
        public static IServiceCollection AddModules(
            this IServiceCollection services,
            Assembly assembly,
            IConfiguration configuration
        )
        {
            var modules = assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>(); ;

            foreach (var module in modules)
                module.RegisterModule(services, configuration);

            return services;
        }
    }
}
