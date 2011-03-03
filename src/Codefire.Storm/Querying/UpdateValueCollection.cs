using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class UpdateValueCollection : List<UpdateValue>
    {
        public UpdateValueCollection()
            : base()
        {
        }

        public UpdateValue Add(string columnName, object value)
        {
            var item = new UpdateValue();
            item.ColumnName = columnName;
            item.Value = value;
            Add(item);

            return item;
        }
    }
}