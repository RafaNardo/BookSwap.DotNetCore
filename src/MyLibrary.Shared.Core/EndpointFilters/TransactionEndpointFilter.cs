using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Shared.Core.Data.UoW;

namespace MyLibrary.Shared.Core.EndpointFilters
{
    public class TransactionEndpointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            try
            {
                await unitOfWork.BeginTransactionAsync();

                var result = await next.Invoke(context);

                await unitOfWork.SaveChangesAsync();

                await unitOfWork.CommitTransactionAsync();

                return result;

            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();

                throw;
            }
        }
    }
}
