using Microsoft.EntityFrameworkCore;

namespace BookSwap.Shared.Data.UoW
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
