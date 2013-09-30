using System.Collections.Generic;
using System.Linq;

namespace Code.Pizza.Common.Paging
{
    public static class PagedCollectionExtensions
    {
        public static PagedSet<T> ToPagedSet<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            IEnumerable<T> enumerable = source as T[] ?? source.ToArray();

            IEnumerable<T> subSet = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            return new PagedSet<T>(subSet, page, pageSize, enumerable.Count());
        }
    }
}
