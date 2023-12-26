using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyLibrary.Shared.Core.Data.UoW
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext) => _dbContext = dbContext;

        public async Task<IExecutionStrategy> CreateExecutionStrategy() 
        {
            return await Task.FromResult(_dbContext.Database.CreateExecutionStrategy());
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
