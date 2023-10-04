using BookSwap.Shared.Core.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookSwap.Shared.Core.DI;

public static class EndpointsInjection
{
    private static List<Type> EndpointTypes { get; } = LoadEndpointTypes();
    
    private static List<IEndpoint> Endpoints => new();

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        foreach (var endpoint in EndpointTypes)
            services.AddTransient(endpoint);

        return services;
    }

    public static WebApplication UseEndpoints(this WebApplication builder)
    {
        var scopedService = builder
            .Services
            .CreateScope()
            .ServiceProvider;

        foreach (var endpointType in EndpointTypes)
        {
            var endpoint = (IEndpoint)scopedService.GetRequiredService(endpointType);

            endpoint
                .MapEndpoint(builder)
                .AddEndpointFilter(async (filterContext, next) => { 
                    
                    BindEndpointProperties(filterContext.HttpContext, endpoint);

                    return await next.Invoke(filterContext);
                });

            Endpoints.Add(endpoint);
        }

        return builder;
    }

    private static void BindEndpointProperties(HttpContext context, IEndpoint endpoint)
    {
        var type = endpoint.GetType();

        var properties = type.GetRuntimeProperties();

        foreach (var property in properties)
        {
            var service = context.RequestServices.GetService(property.PropertyType);

            if (service == null) continue;

            property.SetValue(endpoint, service, null);
        }

        var fields = type.GetRuntimeFields();

        foreach (var field in fields)
        {
            var service = context.RequestServices.GetService(field.FieldType);

            if (service == null) continue;

            field.SetValue(endpoint, service, BindingFlags.Public | BindingFlags.Instance, null, null);
        }
    }

    private static List<Type> LoadEndpointTypes()
    {
        var assembly = Assembly.GetEntryAssembly();
        
        var endpoints = assembly!
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpoint)))
            .Where(t => !t.IsAbstract)
            .ToList();
        
        return endpoints;
    }
}