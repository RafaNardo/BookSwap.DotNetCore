using System.Reflection;
using BookSwap.Shared.Core.Data.Transactions;
using BookSwap.Shared.Core.Data.UoW;
using MediatR;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookSwap.Shared.Core.Mediator
{
    public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var useTransaction = typeof(TRequest).IsAssignableTo(typeof(ITransactionableRequest));

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
