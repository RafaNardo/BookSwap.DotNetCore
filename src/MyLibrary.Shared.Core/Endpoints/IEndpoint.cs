using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace MyLibrary.Shared.Core.Endpoints
{
    public interface IEndpoint
    {
        IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder builder);
    }
}
