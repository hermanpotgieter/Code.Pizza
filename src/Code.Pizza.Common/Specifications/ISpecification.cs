using System;
using System.Linq.Expressions;

namespace Code.Pizza.Common.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> GetExpression();
    }
}
