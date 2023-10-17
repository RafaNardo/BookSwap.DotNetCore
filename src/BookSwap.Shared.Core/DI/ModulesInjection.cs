using BookSwap.Shared.Core.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookSwap.Shared.Core.DI
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
