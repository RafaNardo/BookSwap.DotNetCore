using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace BookSwap.Shared.Core.Endpoints
{
    public interface IEndpoint
    {
        IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder);
    }
}