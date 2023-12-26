using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Shared.Core.Data.UoW;

namespace MyLibrary.Shared.Core.EndpointFilters
{
    public class TransactionEndpointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

            var strategy = await unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        var result = await next.Invoke(context);

                        await unitOfWork.SaveChangesAsync();

                        await transaction.CommitAsync();

                        return result;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }
    }
}
