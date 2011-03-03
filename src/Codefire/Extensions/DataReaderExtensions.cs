using System;
using System.Data;

namespace Codefire.Extensions
{
    public static class DataReaderExtensions
    {
        public static T Get<T>(this IDataReader reader, string key)
        {
            return Get<T>(reader, key, default(T));
        }

        public static T Get<T>(this IDataReader reader, string key, T defaultValue)
        {
            T value = defaultValue;

            if (reader[key] != DBNull.Value)
            {
                value = (T)reader[key];
            }

            return value;
        }
    }
}