using System.Linq.Expressions;

namespace BookSwap.Shared.Core.Data
{
    public static class EfCoreExtensions
    {
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            bool condition)
        {
            if (condition)
            {
                return source.Where(predicate);
            }

            return source;
        }
    }
}
