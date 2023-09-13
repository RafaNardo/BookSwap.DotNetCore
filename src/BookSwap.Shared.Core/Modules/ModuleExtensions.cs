using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookSwap.Shared.Core.Modules
{
    public static class ModuleExtensions
    {
        // this could also be added into the DI container
        static readonly List<IModule> registeredModules = new List<IModule>();

        public static IServiceCollection RegisterModules(
            this IServiceCollection services,
            Assembly assembly,
            IConfiguration configuration
        )
        {
            var modules = DiscoverModules(assembly);
            
            foreach (var module in modules)
            {
                module.RegisterModule(services, configuration);
                registeredModules.Add(module);
            }

            return services;
        }

        public static WebApplication UseModules(this WebApplication app)
        {
            foreach (var module in registeredModules)
            {
                module.MapEndpoints(app);
            }

            return app;
        }

        private static IEnumerable<IModule> DiscoverModules(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
        }
    }
}
