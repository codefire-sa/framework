using System;

namespace Codefire.Storm.Querying
{
    public class UpdateValue
    {
        private string _columnName;
        private object _value;

        public UpdateValue()
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