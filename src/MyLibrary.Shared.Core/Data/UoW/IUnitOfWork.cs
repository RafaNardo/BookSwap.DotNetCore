namespace MyLibrary.Shared.Core.Data.UoW
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
