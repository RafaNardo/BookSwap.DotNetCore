using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BookSwap.Shared.Core.EndpointFilters
{
    public static class EndpointFilterExtensions
    {
        public static RouteHandlerBuilder WithTransaction(this RouteHandlerBuilder builder)
            => builder.AddEndpointFilter<TransactionEndpointFilter>();

        public static RouteHandlerBuilder WithValidator<T>(this RouteHandlerBuilder builder)
            => builder.AddEndpointFilter<ValidationFilter<T>>();
    }
}
