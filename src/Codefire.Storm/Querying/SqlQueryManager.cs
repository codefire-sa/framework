using System;

namespace Codefire.Storm.Querying
{
    public class SqlQueryManager
    {
        private IDataProvider _provider;

        public SqlQueryManager(IDataProvider provider)
        {
            _provider = provider;
        }

        public SqlSelectQuery Select(string tableName)
        {
            var qry = new SqlSelectQuery(_provider);
            qry.From(tableName);

            return qry;
        }

        public SqlInsertQuery Insert(string tableName)
        {
            var qry = new SqlInsertQuery(_provider);
            qry.Into(tableName);

            return qry;
        }

        public SqlUpdateQuery Update(string tableName)
        {
            var qry = new SqlUpdateQuery(_provider);
            qry.Update(tableName);

            return qry;
        }

        public SqlDeleteQuery Delete(string tableName)
        {
            var qry = new SqlDeleteQuery(_provider);
            qry.From(tableName);

            return qry;
        }
    }
}
