using System;
using System.Data;

namespace Codefire.Extensions
{
    public static class DataTableExtensions
    {
        public static T Get<T>(this DataRow row, string key)
        {
            return Get<T>(row, key, default(T));
        }

        public static T Get<T>(this DataRow row, string key, T defaultValue)
        {
            T value = defaultValue;

            if (row[key] != DBNull.Value)
            {
                value = (T)row[key];
            }

            return value;
        }
    }
}