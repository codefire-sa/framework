using System;

namespace Codefire.Storm.Querying
{
    public class EntityQueryValues
    {
        private int _limit;
        private int _pageNumber;
        private int _pageSize;
        private CriteriaCollection _criteriaList;
        private OrderCollection _orderList;

        public EntityQueryValues()
        {
            _criteriaList = new CriteriaCollection();
            _orderList = new OrderCollection();
        }

        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public CriteriaCollection Criteria
        {
            get { return _criteriaList; }
        }

        public OrderCollection OrderBy
        {
            get { return _orderList; }
        }
    }
}