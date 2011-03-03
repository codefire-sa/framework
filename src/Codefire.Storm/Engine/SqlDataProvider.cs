using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Engine
{
    public class SqlDataProvider : IDataProvider
    {
        private DbProviderFactory _factory;
        private DbConnection _connection;
        private DbTransaction _transaction;
        private SqlGenerator _generator;

        public SqlDataProvider()
        {
            _factory = SqlClientFactory.Instance;
            _connection = _factory.CreateConnection();
            _generator = new SqlGenerator(this);
        }

        public DbConnection Connection
        {
            get { return _connection; }
        }

        public DbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }

            _connection.Close();
        }

        public void BeginTransaction(IsolationLevel level)
        {
            if (_transaction != null) return;

            OpenConnection();
            _transaction = _connection.BeginTransaction(level);
        }

        public void CommitTransaction()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public DbCommand CreateCommand()
        {
            var cmd = _factory.CreateCommand();
            cmd.Connection = _connection;
            cmd.Transaction = _transaction;

            return cmd;
        }

        public DbDataAdapter CreateDataAdapter()
        {
            return _factory.CreateDataAdapter();
        }

        public string QualifyTableName(string schema, string table)
        {
            string format;
            if (string.IsNullOrEmpty(schema))
            {
                format = "[{1}]";
            }
            else
            {
                format = "[{0}].[{1}]";
            }

            return string.Format(format, schema, table);
        }

        public string QualifyColumnName(string schema, string table, string column)
        {
            string format;
            if (string.IsNullOrEmpty(schema))
            {
                format = "[{1}].[{2}]";
            }
            else
            {
                format = "[{0}].[{1}].[{2}]";
            }

            return string.Format(format, schema, table, column);
        }

        public string QualifyProcedureName(string schema, string procedure)
        {
            string format;
            if (string.IsNullOrEmpty(schema))
            {
                format = "[{1}]";
            }
            else
            {
                format = "[{0}].[{1}]";
            }

            return string.Format(format, schema, procedure);
        }

        public string GetParameterName(string name)
        {
            return name.StartsWith("@") ? name : "@" + name;
        }

        public IDataCommand Generate(QueryTemplate template)
        {
            return _generator.Build(template);
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}