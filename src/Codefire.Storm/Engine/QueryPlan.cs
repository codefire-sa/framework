using System;
using System.Collections.Generic;
using System.Text;
using Codefire.Storm.Querying;
using System.Reflection;

namespace Codefire.Storm.Engine
{
    public class QueryPlan
    {
        private ColumnDescriptor _id;
        private List<ColumnDescriptor> _columns;
        private string _tableName;
        private string _tableAlias;
        private List<JoinDescriptor> _joins;

        public QueryPlan()
        {
            _columns = new List<ColumnDescriptor>();
            _joins = new List<JoinDescriptor>();
        }

        public ColumnDescriptor Id
        {
            get { return _id; }
        }

        public List<ColumnDescriptor> Columns
        {
            get { return _columns; }
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

        public List<JoinDescriptor> Joins
        {
            get { return _joins; }
        }

        public ColumnDescriptor AddId(string memberName, string columnName, string columnAlias, PropertyInfo[] accessors)
        {
            _id = AddColumn(memberName, columnName, columnAlias, accessors);

            return _id;
        }

        public ColumnDescriptor AddColumn(string memberName, string columnName, string columnAlias, PropertyInfo[] accessors)
        {
            var item = new ColumnDescriptor();
            item.MemberName = memberName;
            item.ColumnName = columnName;
            item.ColumnAlias = columnAlias;
            item.Accessors = accessors;
            _columns.Add(item);

            return item;
        }

        public ColumnDescriptor GetColumn(string memberName)
        {
            return _columns.Find(x => x.MemberName == memberName);
        }

        public JoinDescriptor AddJoin(string tableName, string tableAlias, string leftColumnName, string rightColumnName, JoinType joinType)
        {
            var item = new JoinDescriptor();
            item.TableName = tableName;
            item.TableAlias = tableAlias;
            item.LeftColumnName = leftColumnName;
            item.RightColumnName = rightColumnName;
            item.JoinType = joinType;

            _joins.Add(item);

            return item;
        }

        public string BuildSelectColumns()
        {
            var builder = new StringBuilder();

            var delimiter = "";
            foreach (var columnItem in _columns)
            {
                builder.AppendFormat("{0}{1} AS {2}", delimiter, columnItem.ColumnName, columnItem.ColumnAlias);
                delimiter = ",";
            }

            return builder.ToString();
        }
    }
}