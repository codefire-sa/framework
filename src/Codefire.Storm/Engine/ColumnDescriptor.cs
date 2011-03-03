using System;
using System.Reflection;

namespace Codefire.Storm.Engine
{
    public class ColumnDescriptor
    {
        private string _memberName;
        private string _columnName;
        private string _columnAlias;
        private PropertyInfo[] _accessors;

        public string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; }
        }

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        public string ColumnAlias
        {
            get { return _columnAlias; }
            set { _columnAlias = value; }
        }

        public PropertyInfo[] Accessors
        {
            get { return _accessors; }
            set { _accessors = value; }
        }
    }
}