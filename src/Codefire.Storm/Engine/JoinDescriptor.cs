using System;
using System.Collections.Generic;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Engine
{
    public class JoinDescriptor
    {
        private string _tableName;
        private string _tableAlias;
        private string _leftColumnName;
        private string _rightColumnName;
        private JoinType _joinType;

        public JoinDescriptor()
        {
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string TableAlias
        {
            get { return _tableAlias; }
            set { _tableAlias = value; }
        }

        public string LeftColumnName
        {
            get { return _leftColumnName; }
            set { _leftColumnName = value; }
        }

        public string RightColumnName
        {
            get { return _rightColumnName; }
            set { _rightColumnName = value; }
        }

        public JoinType JoinType
        {
            get { return _joinType; }
            set { _joinType = value; }
        }
    }
}