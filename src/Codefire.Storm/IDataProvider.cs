using System;
using System.Data;
using System.Data.Common;
using Codefire.Storm.Querying;

namespace Codefire.Storm
{
    public interface IDataProvider : IDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }

        void OpenConnection();
        void CloseConnection();
        void BeginTransaction(IsolationLevel level);
        void CommitTransaction();
        void RollbackTransaction();
        DbCommand CreateCommand();
        DbDataAdapter CreateDataAdapter();

        string QualifyTableName(string schema, string table);
        string QualifyColumnName(string schema, string table, string column);
        string QualifyProcedureName(string schema, string procedure);
        string GetParameterName(string name);
        IDataCommand Generate(QueryTemplate template);
    }
}
