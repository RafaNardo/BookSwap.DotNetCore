using System.Linq.Expressions;

namespace MyLibrary.Shared.Core.Data.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        private bool _hasOrder = false;
        public List<Expression<Func<T, bool>>> Criterias { get; private set; } = new();
        public List<Expression<Func<T, object>>> Includes { get; private set; } = new();
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; private set; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public Specification<T> AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public Specification<T> AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criterias.Add(criteria);
            return this;
        }

        public Specification<T> AddCriteriaIf(Expression<Func<T, bool>> criteria, bool condition)
        {
            if (condition)
                Criterias.Add(criteria);

            return this;
        }

        public Specification<T> AddSkip(int skip)
        {
            Skip = skip;
            return this;
        }

        public Specification<T> AddTake(int take)
        {
            Take = take;
            return this;
        }

        public Specification<T> AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            if (_hasOrder)
            {
                OrderBy = q => ((IOrderedQueryable<T>)q).ThenBy(orderByExpression);
            }
            else
            {
                OrderBy = q => q.OrderBy(orderByExpression);
                _hasOrder = true;
            }

            return this;
        }

        public Specification<T> AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            if (_hasOrder)
            {
                OrderBy = q => ((IOrderedQueryable<T>)q).ThenByDescending(orderByDescendingExpression);
            }
            else
            {
                OrderBy = q => q.OrderBy(orderByDescendingExpression);
                _hasOrder = true;
            }

            return this;
        }
    }
}
