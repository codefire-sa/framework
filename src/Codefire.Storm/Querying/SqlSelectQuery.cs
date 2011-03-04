using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Querying
{
    public class SqlSelectQuery : SqlQuery
    {
        public SqlSelectQuery(IDataProvider provider)
            : base(provider, QueryType.Select)
        {
        }

        public SqlSelectQuery Top(int limit)
        {
            Template.Limit = limit;

            return this;
        }

        public SqlSelectQuery Paged(int pageNumber, int pageSize)
        {
            Template.PageNumber = pageNumber;
            Template.PageSize = pageSize;

            return this;
        }

        /// <summary>
        /// Sets that all columns need to be returned in the select.
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Columns()
        {
            Template.SelectColumns = "*";

            return this;
        }

        /// <summary>
        /// Sets the list of columns to be returned in the select.
        /// </summary>
        /// <param name="columns">A comma delimited list of columns.</param>
        /// <returns></returns>
        public SqlSelectQuery Columns(string columns)
        {
            Template.SelectColumns = columns;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Count()
        {
            Template.AggregateType = AggregateType.Count;
            Template.AggregateColumn = "*";

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Min(string column)
        {
            Template.AggregateType = AggregateType.Min;
            Template.AggregateColumn = column;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Max(string column)
        {
            Template.AggregateType = AggregateType.Max;
            Template.AggregateColumn = column;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Avg(string column)
        {
            Template.AggregateType = AggregateType.Avg;
            Template.AggregateColumn = column;

            return this;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public SqlSelectQuery Sum(string column)
        {
            Template.AggregateType = AggregateType.Sum;
            Template.AggregateColumn = column;

            return this;
        }

        public SqlSelectQuery From(string tableName)
        {
            Template.TableName = tableName;
            Template.TableAlias = null;

            return this;
        }

        /// <summary>
        /// Sets the name and alias of the table (or view) to select data from.
        /// </summary>
        /// <param name="tableName">The name of the table or view.</param>
        /// <param name="aliasName">The alias name to use for the table or view.</param>
        /// <returns></returns>
        public SqlSelectQuery From(string tableName, string aliasName)
        {
            Template.TableName = tableName;
            Template.TableAlias = aliasName;

            return this;
        }

        #region [ Joins ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery InnerJoin(string tableName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.Inner, tableName, null, leftColumn, rightColumn);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="aliasName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery InnerJoin(string tableName, string aliasName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.Inner, tableName, aliasName, leftColumn, rightColumn);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery LeftOuterJoin(string tableName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.LeftOuter, tableName, null, leftColumn, rightColumn);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="aliasName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery LeftOuterJoin(string tableName, string aliasName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.LeftOuter, tableName, aliasName, leftColumn, rightColumn);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery RightOuterJoin(string tableName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.RightOuter, tableName, null, leftColumn, rightColumn);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="aliasName"></param>
        /// <param name="leftColumn"></param>
        /// <param name="rightColumn"></param>
        /// <returns></returns>
        public SqlSelectQuery RightOuterJoin(string tableName, string aliasName, string leftColumn, string rightColumn)
        {
            Template.Joins.Add(JoinType.RightOuter, tableName, aliasName, leftColumn, rightColumn);

            return this;
        }

        #endregion

        #region [ Criteria ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SqlSelectQuery Where(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SqlSelectQuery And(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public SqlSelectQuery Or(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.Or;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        #endregion

        #region [ Order By ]

        /// <summary>
        /// Sets the list of columns to be ordered by in the select.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public SqlSelectQuery OrderAsc(params string[] columnList)
        {
            foreach (var columnName in columnList)
            {
                Template.OrderBy.Add(null, columnName, true);
            }

            return this;
        }

        /// <summary>
        /// Sets the list of columns to be ordered by in the select.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public SqlSelectQuery OrderDesc(params string[] columnList)
        {
            foreach (var columnName in columnList)
            {
                Template.OrderBy.Add(null, columnName, false);
            }

            return this;
        }

        #endregion
    }
}