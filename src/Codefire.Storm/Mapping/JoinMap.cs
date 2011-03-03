using System;
using System.Collections.Generic;

namespace Codefire.Storm.Mapping
{
    public class JoinMap
    {
        private string _tableName;
        private string _parentColumn;
        private string _childColumn;
        private List<PropertyMap> _properties;

        public JoinMap()
        {
            _properties = new List<PropertyMap>();
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string ParentColumn
        {
            get { return _parentColumn; }
            set { _parentColumn = value; }
        }

        public string ChildColumn
        {
            get { return _childColumn; }
            set { _childColumn = value; }
        }

        public List<PropertyMap> Properties
        {
            get { return _properties; }
        }
    }
}