using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class InsertValueCollection : List<InsertValue>
    {
        public InsertValueCollection()
            : base()
        {
        }

        public InsertValue Add(string columnName, object value)
        {
            var item = new InsertValue();
            item.ColumnName = columnName;
            item.Value = value;
            Add(item);

            return item;
        }
    }
}