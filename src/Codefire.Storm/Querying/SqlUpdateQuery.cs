using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Querying
{
    public class SqlUpdateQuery : SqlQuery
    {
        public SqlUpdateQuery(IDataProvider provider)
            : base(provider, QueryType.Update)
        {
        }

        public SqlUpdateQuery Update(string tableName)
        {
            Template.TableName = tableName;

            return this;
        }

        public SqlUpdateQuery Set(string columnName, object value)
        {
            Template.UpdateValues.Add(columnName, value);

            return this;
        }

        public SqlUpdateQuery Where(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        public SqlUpdateQuery And(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        public SqlUpdateQuery Or(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.Or;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }
    }
}