using System;
using System.Collections.Generic;
using System.Reflection;

namespace Codefire.Storm.Mapping
{
    public class PropertyMap
    {
        private string _memberName;
        private PropertyInfo[] _accessors;
        private string _columnName;
        private ColumnOptions _options;

        public PropertyMap()
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

        public ColumnOptions Options
        {
            get { return _options; }
            set { _options = value; }
        }
    }
}