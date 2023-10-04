using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSwap.Shared.Core.Modules
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection builder, IConfiguration configuration);
    }
}
