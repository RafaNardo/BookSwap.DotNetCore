namespace BookSwap.Shared.Core.Mediator
{
    public interface ICacheableRequest
    {
        string CacheKey { get; }
        bool BypassCache { get; }
        TimeSpan SlidingExpiration { get; }
    }
}
