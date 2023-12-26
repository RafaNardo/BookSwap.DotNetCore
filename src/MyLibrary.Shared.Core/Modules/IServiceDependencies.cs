using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace MyLibrary.Shared.Core.Modules
{
    public interface IServiceDependencies
    {
        WebApplicationBuilder RegisterModule(WebApplicationBuilder builder, IConfiguration configuration);
    }
}
