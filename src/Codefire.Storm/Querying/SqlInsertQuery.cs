using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Querying
{
    public class SqlInsertQuery : SqlQuery
    {
        public SqlInsertQuery(IDataProvider provider)
            : base(provider, QueryType.Insert)
        {
        }

        public SqlInsertQuery Into(string tableName)
        {
            Template.TableName = tableName;

            return this;
        }

        public SqlInsertQuery Value(string columnName, object value)
        {
            Template.InsertValues.Add(columnName, value);

            return this;
        }

        public SqlInsertQuery SelectIdentity()
        {
            Template.SelectIdentity = true;

            return this;
        }
    }
}