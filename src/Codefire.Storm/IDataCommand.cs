using System;
using System.Data;

namespace Codefire.Storm
{
    public interface IDataCommand
    {
        IDataProvider Provider { get; }
        CommandType CommandType { get; set; }
        string CommandText { get; set; }

        void AddInParameter(string name, DbType dataType, int size, object value);
        void AddInParameter(string name, DbType dataType, byte precision, byte scale, object value);
        void AddInParameter(string name, DbType dataType, object value);
        void AddOutParameter(string name, DbType dataType, byte precision, byte scale, object value);
        void AddOutParameter(string name, DbType dataType, int size, object value);
        void AddOutParameter(string name, DbType dataType, object value);
        void AddParameter(string name, DbType dataType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, object value);
        void AddParameter(string name, object value);
        T GetParameterValue<T>(string name);
        void SetParameterValue(string name, object value);
        int GetReturnValue();

        int ExecuteNonQuery();
        IDataReader ExecuteReader();
        object ExecuteScalar();
        T ExecuteScalar<T>();
        DataTable FillTable();
        DataTable FillTable(string tableName);
    }
}
