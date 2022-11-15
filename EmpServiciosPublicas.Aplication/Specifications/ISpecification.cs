using System.Linq.Expressions;

namespace EmpServiciosPublicas.Aplication.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        bool IgnoreQueryFilters { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnable { get; }
    }
}
