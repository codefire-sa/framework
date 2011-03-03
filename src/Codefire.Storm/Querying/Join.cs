using System;

namespace Codefire.Storm.Querying
{
    public class Join
    {
        #region [ Fields ]

        private JoinType _joinType;
        private string _tableName;
        private string _aliasName;
        private string _leftColumn;
        private string _rightColumn;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// 
        /// </summary>
        public Join()
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// 
        /// </summary>
        public JoinType JoinType
        {
            get { return _joinType; }
            set { _joinType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string LeftColumn
        {
            get { return _leftColumn; }
            set { _leftColumn = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RightColumn
        {
            get { return _rightColumn; }
            set { _rightColumn = value; }
        }

        #endregion
    }
}