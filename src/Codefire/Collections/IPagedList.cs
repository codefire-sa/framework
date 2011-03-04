using System;
using System.Collections.Generic;

namespace Codefire.Collections
{
    public interface IPagedList
    {
        int TotalPages { get; }
        int TotalCount { get; }
        int PageNumber { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
    }
}