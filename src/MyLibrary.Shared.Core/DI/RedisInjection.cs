using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace MyLibrary.Shared.Core.DI
{
    public static class RedisInjection
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, Assembly assembly, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = assembly.GetName().Name!;

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { configuration.GetValue<string>("DistributedCache:Server"), configuration.GetValue<string>("DistributedCache:Port") },
                    Password = configuration.GetValue<string>("DistributedCache:Password"),
                    ConnectRetry = 5,
                    ReconnectRetryPolicy = new LinearRetry(1500),
                    Ssl = configuration.GetValue<bool>("DistributedCache:UseSsl"),
                    AbortOnConnectFail = false,
                    ConnectTimeout = 5000,
                    SyncTimeout = 5000,
                    DefaultDatabase = 0
                };
            });

            return services;
        }
    }
}

