using BookSwap.Shared.Data.UoW;
using MediatR;
using System.Reflection;

namespace BookSwap.Shared.Data.Transactions
{
    public class UseTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UseTransactionPipelineBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var useTransaction = typeof(TRequest).GetCustomAttribute<UseTransactionAttribute>() != null;

            if (useTransaction)
            {
                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    var response = await next();
                    
                    await _unitOfWork.CommitTransactionAsync();
                    
                    return response;
                }
                catch (Exception)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }

            return await next();
        }
    }
}
