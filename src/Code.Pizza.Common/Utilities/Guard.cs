using System;
using System.Linq.Expressions;

namespace Code.Pizza.Common.Utilities
{
    public class Guard
    {
        public static void Against<TException>(bool assertion, string message) where TException : Exception
        {
            if(assertion)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static void Against<TException>(Func<bool> assertion, string message) where TException : Exception
        {
            if(assertion())
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static void AgainstNull<T>(Expression<Func<T>> expression, string message = null)
        {
            string argument = expression.Body is MemberExpression ? ((MemberExpression)expression.Body).Member.Name : "Argument";

            if(Equals(expression.Compile().Invoke(), null))
            {
                if(message == null)
                {
                    throw new ArgumentNullException(argument);
                }

                throw new ArgumentNullException(argument, message);
            }
        }

        public static void AgainstNullOrEmptyString(Expression<Func<string>> expression, string message = null)
        {
            string argument = expression.Body is MemberExpression
                                  ? ((MemberExpression)expression.Body).Member.Name
                                  : "Expression evaluates to null";

            if(string.IsNullOrEmpty(expression.Compile().Invoke()))
            {
                if(string.IsNullOrWhiteSpace(message))
                {
                    message = string.Format("String cannot be null or empty: {0}", argument);
                }

                throw new ArgumentException(message);
            }
        }

        public static void AgainstNullOrWhiteSpaceString(Expression<Func<string>> expression, string message = null)
        {
            string argument = expression.Body is MemberExpression
                                  ? ((MemberExpression)expression.Body).Member.Name
                                  : "Expression evaluates to null";

            if(string.IsNullOrWhiteSpace(expression.Compile().Invoke()))
            {
                if(string.IsNullOrWhiteSpace(message))
                {
                    message = string.Format("String cannot be null or whitespace: {0}", argument);
                }

                throw new ArgumentException(message);
            }
        }
    }
}
