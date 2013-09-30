using System;
using System.Collections.Generic;
using System.Linq;
using Code.Pizza.Common.Utilities;

namespace Code.Pizza.Common.Paging
{
    public class PagedSet<T> : HashSet<T>, IPagedCollection<T>
    {
        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int ItemCount { get; private set; }

        public int PageCount { get; private set; }

        public bool IsFirstPage
        {
            get
            {
                return this.Page == 1;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return this.Page == this.PageCount;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return this.Page > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return this.Page < this.PageCount;
            }
        }

        public int NextPage
        {
            get
            {
                if (this.HasNextPage)
                    return this.Page + 1;
                return this.PageCount;
            }
        }

        public PagedSet(IEnumerable<T> source, int page, int pageSize, int itemCount)
        {
            Guard.AgainstNull(() => source);

            IEnumerable<T> enumerable = source as T[] ?? source.ToArray();

            Guard.Against<ArgumentOutOfRangeException>(page < 1, "Page cannot be less than 1.");
            Guard.Against<ArgumentOutOfRangeException>(pageSize < 1, "PageSize cannot be less than 1.");
            Guard.Against<ArgumentOutOfRangeException>(itemCount < 0, "ItemCount cannot be less than 0.");
            Guard.Against<ArgumentOutOfRangeException>(itemCount < enumerable.Count(), "ItemCount cannot be less than source.Count().");

            this.Page = page;
            this.PageSize = pageSize;
            this.ItemCount = itemCount;

            this.PageCount = itemCount > 0 ? (int)Math.Ceiling(this.ItemCount / (double)this.PageSize) : 1;

            Guard.Against<ArgumentOutOfRangeException>(page > this.PageCount, "Page cannot be greater than PageCount.");

            this.UnionWith(enumerable);
        }
    }
}
