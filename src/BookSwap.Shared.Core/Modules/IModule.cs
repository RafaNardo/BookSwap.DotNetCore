using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSwap.Shared.Core.Modules
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration);
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
