using System;

namespace Codefire.Storm.Querying
{
    public class InsertValue
    {
        private string _columnName;
        private object _value;

        public InsertValue()
        {
        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}