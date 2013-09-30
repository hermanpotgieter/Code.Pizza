using System;
using System.Linq.Expressions;

namespace Code.Pizza.Common.Specifications
{
    public class AdHocSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>> expression;

        public AdHocSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return this.expression;
        }
    }
}
