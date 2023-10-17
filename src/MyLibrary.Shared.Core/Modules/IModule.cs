using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyLibrary.Shared.Core.Modules
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration);
    }
}
