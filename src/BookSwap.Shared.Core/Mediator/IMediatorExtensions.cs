using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookSwap.Shared.Core.Mediator
{
    public static class IMediatorExtensions
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            var assembly = Assembly.GetEntryAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly!));
            return services;
        }
    }
}
