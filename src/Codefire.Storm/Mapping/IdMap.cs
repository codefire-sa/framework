using System;
using System.Reflection;

namespace Codefire.Storm.Mapping
{
    public class IdMap
    {
        private string _memberName;
        private PropertyInfo[] _accessors;
        private string _columnName;
        private bool _isAutoIncrement;

        public IdMap()
        {
        }

        public string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; }
        }

        public PropertyInfo[] Accessors
        {
            get { return _accessors; }
            set { _accessors = value; }
        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        public bool IsAutoIncrement
        {
            get { return _isAutoIncrement; }
            set { _isAutoIncrement = value; }
        }
    }
}