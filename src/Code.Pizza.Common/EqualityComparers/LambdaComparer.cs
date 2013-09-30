using System;
using System.Collections;
using System.Collections.Generic;
using Code.Pizza.Common.Utilities;

namespace Code.Pizza.Common.EqualityComparers
{
    public class LambdaComparer<T> : IEqualityComparer<T>, IEqualityComparer
    {
        private readonly Func<T, T, bool> comparer;

        public LambdaComparer(Func<T, T, bool> comparer)
        {
            Guard.Against<ArgumentNullException>(comparer == null, "Comparer cannot be null.");

            this.comparer = comparer;
        }

        bool IEqualityComparer.Equals(object x, object y)
        {
            if(x is T && y is T)
            {
                bool equals = this.comparer((T)x, (T)y);

                return equals;
            }

            return false;
        }

        int IEqualityComparer.GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            bool equals = this.comparer(x, y);

            return equals;
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
