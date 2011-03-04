using System;
using System.Collections.Generic;

namespace Codefire.Collections
{
    public class PagedList<T> : List<T>, IPagedList
    {
        private int _totalCount;
        private int _totalPages;
        private int _pageNumber;
        private int _pageSize;

        public PagedList(IEnumerable<T> source, int totalCount, int pageNumber, int pageSize)
        {
            _totalCount = totalCount;
            _totalPages = totalCount / pageSize;
            if (totalCount % pageSize > 0) _totalPages++;
            _pageNumber = pageNumber;
            _pageSize = pageSize;

            base.AddRange(source);
        }

        public int TotalCount
        {
            get { return _totalCount; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
        }

        public int PageNumber
        {
            get { return _pageNumber; }
        }

        public int PageSize
        {
            get { return _pageSize; }
        }

        public bool HasPreviousPage
        {
            get { return (_pageNumber > 1); }
        }

        public bool HasNextPage
        {
            get { return (_pageNumber < _totalPages); }
        }

        public bool IsFirstPage
        {
            get { return (_pageNumber <= 1); }
        }

        public bool IsLastPage
        {
            get { return (_pageNumber >= _totalPages); }
        }
    }
}