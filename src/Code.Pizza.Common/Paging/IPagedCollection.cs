using System.Collections.Generic;

namespace Code.Pizza.Common.Paging
{
    public interface IPagedCollection<T> : ICollection<T>
    {
        int Page { get; }

        int PageSize { get; }

        int ItemCount { get; }

        int PageCount { get; }

        bool IsFirstPage { get; }

        bool IsLastPage { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        int NextPage { get; }
    }
}