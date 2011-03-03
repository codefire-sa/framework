using System;
using System.Data;
using System.Data.Common;

namespace Codefire.Storm
{
    public class StormCommand : IDataCommand
    {
        private IDataProvider _provider;
        private DbCommand _command;

        public StormCommand(IDataProvider provider)
        {
            _provider = provider;

            _command = provider.CreateCommand();
            _command.Connection = provider.Connection;
            _command.Transaction = provider.Transaction;

            AddParameter("@RETURN_VALUE", DbType.Int32, 0, ParameterDirection.ReturnValue, true, 0, 0, 0);
        }

        public IDataProvider Provider
        {
            get { return _provider; }
        }

        public string CommandText
        {
            get { return _command.CommandText; }
            set { _command.CommandText = value; }
        }

        public CommandType CommandType
        {
            get { return _command.CommandType; }
            set { _command.CommandType = value; }
        }

        public void AddInParameter(string name, DbType dataType, object value)
        {
            AddParameter(name, dataType, 0, ParameterDirection.Input, true, 0, 0, value);
        }

        public void AddInParameter(string name, DbType dataType, byte precision, byte scale, object value)
        {
            AddParameter(name, dataType, 0, ParameterDirection.Input, true, precision, scale, value);
        }

        public void AddInParameter(string name, DbType dataType, int size, object value)
        {
            AddParameter(name, dataType, size, ParameterDirection.Input, true, 0, 0, value);
        }

        public void AddOutParameter(string name, DbType dataType, object value)
        {
            AddParameter(name, dataType, 0, ParameterDirection.InputOutput, true, 0, 0, value);
        }

        public void AddOutParameter(string name, DbType dataType, byte precision, byte scale, object value)
        {
            AddParameter(name, dataType, 0, ParameterDirection.InputOutput, true, precision, scale, value);
        }

        public void AddOutParameter(string name, DbType dataType, int size, object value)
        {
            AddParameter(name, dataType, size, ParameterDirection.InputOutput, true, 0, 0, value);
        }

        public void AddParameter(string name, object value)
        {
            var param = _command.CreateParameter();

            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;

            _command.Parameters.Add(param);
        }
        
        public void AddParameter(string name, DbType dataType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, object value)
        {
            var param = _command.CreateParameter();

            param.ParameterName = name;
            param.DbType = dataType;
            param.Size = size;
            param.Direction = direction;
            param.Value = value ?? DBNull.Value;
            param.IsNullable = nullable;

            var dbParam = param as IDbDataParameter;
            if (dbParam != null)
            {
                dbParam.Precision = precision;
                dbParam.Scale = scale;
            }

            _command.Parameters.Add(param);
        }

        public T GetParameterValue<T>(string name)
        {
            T value = default(T);

            foreach (IDataParameter paramItem in _command.Parameters)
            {
                if (paramItem.ParameterName == name)
                {
                    if (paramItem.Value != DBNull.Value)
                    {
                        value = (T)paramItem.Value;
                    }
                    break;
                }
            }

            return value;
        }

        public void SetParameterValue(string name, object value)
        {
            foreach (IDataParameter paramItem in _command.Parameters)
            {
                if (paramItem.ParameterName == name)
                {
                    paramItem.Value = value;
                    break;
                }
            }
        }

        public int GetReturnValue()
        {
            int value = 0;

            foreach (IDataParameter paramItem in _command.Parameters)
            {
                if (paramItem.Direction == ParameterDirection.ReturnValue)
                {
                    value = (int)paramItem.Value;
                    break;
                }
            }

            return value;
        }

        public int ExecuteNonQuery()
        {
            _provider.OpenConnection();

            return _command.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            _provider.OpenConnection();

            return _command.ExecuteScalar();
        }

        public T ExecuteScalar<T>()
        {
            _provider.OpenConnection();

            var value = _command.ExecuteScalar();

            return value == DBNull.Value ? default(T) : (T)value;
        }

        public IDataReader ExecuteReader()
        {
            _provider.OpenConnection();

            return _command.ExecuteReader();
        }

        public DataTable FillTable()
        {
            return FillTable("Table");
        }

        public DataTable FillTable(string tableName)
        {
            var adapter = _provider.CreateDataAdapter();
            adapter.SelectCommand = _command;

            var table = new DataTable(tableName);
            adapter.Fill(table);

            return table;
        }
    }
}