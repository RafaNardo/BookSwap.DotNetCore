using System.Linq.Expressions;

namespace BookSwap.Shared.Core.Data.Specifications
{
    public interface ISpecification<T>
    {
        int? Skip { get; }
        int? Take { get; }
        List<Expression<Func<T, bool>>> Criterias { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; }
        Specification<T> AddInclude(Expression<Func<T, object>> includeExpression);
        Specification<T> AddCriteria(Expression<Func<T, bool>> criteria);
        Specification<T> AddSkip(int skip);
        Specification<T> AddTake(int take);
        Specification<T> AddOrderBy(Expression<Func<T, object>> orderByExpression);
        Specification<T> AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression);
    }
}
