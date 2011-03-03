using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Querying
{
    public class SqlDeleteQuery : SqlQuery
    {
        public SqlDeleteQuery(IDataProvider provider)
            : base(provider, QueryType.Delete)
        {
        }

        public SqlDeleteQuery From(string tableName)
        {
            Template.TableName = tableName;

            return this;
        }

        public SqlDeleteQuery Where(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        public SqlDeleteQuery And(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.And;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }

        public SqlDeleteQuery Or(string columnName, Criteria constraint)
        {
            constraint.CriteriaType = CriteriaType.Or;
            constraint.ColumnName = columnName;
            Template.Criteria.Add(constraint);

            return this;
        }
    }
}