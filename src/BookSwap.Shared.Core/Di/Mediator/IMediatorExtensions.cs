using BookSwap.Shared.Data.Transactions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookSwap.Shared.Core.Di
{
    public static class IMediatorExtensions
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            var assembly = Assembly.GetEntryAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UseTransactionPipelineBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly!));
            return services;
        }
    }

}
