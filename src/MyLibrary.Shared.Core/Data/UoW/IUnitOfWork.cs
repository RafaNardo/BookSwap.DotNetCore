using Microsoft.EntityFrameworkCore.Storage;

namespace MyLibrary.Shared.Core.Data.UoW
{
    public interface IUnitOfWork
    {
        Task<IExecutionStrategy> CreateExecutionStrategy();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
