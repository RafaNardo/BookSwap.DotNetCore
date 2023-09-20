using System.Text;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookSwap.Shared.Core.Mediator
{
    public class CachingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableRequest
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;
        public CachingPipelineBehavior(IDistributedCache cache, ILogger<TResponse> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;
            
            if (request.BypassCache) return await next();

            var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse))!;
                _logger.LogInformation($"Fetched from Cache -> '{request.CacheKey}'.");
            }
            else
            {
                response = await GetResponseAndAddToCache(request, next, cancellationToken);
                _logger.LogInformation($"Added to Cache -> '{request.CacheKey}'.");
            }
            return response;
        }
        
        private async Task<TResponse> GetResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();
            
            var options = new DistributedCacheEntryOptions { SlidingExpiration = request.SlidingExpiration };
            
            var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
            
            await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);
            
            return response;
        }
    }
}
