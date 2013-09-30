using System.Collections.Generic;
using System.Linq.Expressions;

namespace Code.Pizza.Common.Specifications
{
    public class ParameterRewriter : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> dictionary;

        public ParameterRewriter(Dictionary<ParameterExpression, ParameterExpression> dictionary)
        {
            this.dictionary = dictionary ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> dictionary, Expression exp)
        {
            return new ParameterRewriter(dictionary).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            ParameterExpression replacement;

            if (this.dictionary.TryGetValue(parameter, out replacement))
            {
                parameter = replacement;
            }

            return base.VisitParameter(parameter);
        }
    }
}