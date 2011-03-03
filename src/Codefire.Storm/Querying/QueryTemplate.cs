using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class QueryTemplate
    {
        private QueryType _queryType;
        private int _limit;
        private int _pageNumber;
        private int _pageSize;
        private bool _selectIdentity;
        private string _selectColumns;
        private string _tableName;
        private string _tableAlias;
        private InsertValueCollection _insertValues;
        private UpdateValueCollection _updateValues;
        private JoinCollection _joinList;
        private CriteriaCollection _criteriaList;
        private OrderCollection _orderList;

        public QueryTemplate(QueryType type)
        {
            _queryType = type;

            _insertValues = new InsertValueCollection();
            _updateValues = new UpdateValueCollection();
            _joinList = new JoinCollection();
            _criteriaList = new CriteriaCollection();
            _orderList = new OrderCollection();
        }

        public QueryType QueryType
        {
            get { return _queryType; }
            set { _queryType = value; }
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

        public bool SelectIdentity
        {
            get { return _selectIdentity; }
            set { _selectIdentity = value; }
        }

        public string SelectColumns
        {
            get { return _selectColumns; }
            set { _selectColumns = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string TableAlias
        {
            get { return _tableAlias; }
            set { _tableAlias = value; }
        }

        public InsertValueCollection InsertValues
        {
            get { return _insertValues; }
        }

        public UpdateValueCollection UpdateValues
        {
            get { return _updateValues; }
        }

        public JoinCollection Joins
        {
            get { return _joinList; }
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