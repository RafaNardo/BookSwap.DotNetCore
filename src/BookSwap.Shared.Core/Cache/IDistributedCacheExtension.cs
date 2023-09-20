using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookSwap.Shared.Core.Cache
{
    public static class IDistributedCacheExtension
    {
        public static IServiceCollection AddStackExchangeRedis(this IServiceCollection services, IConfiguration configuration, string instance)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = instance;

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
    
