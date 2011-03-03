using System;

namespace Codefire.Storm.Querying
{
    public class Order
    {
        #region [ Fields ]

        private string _columnName;
        private bool _ascending;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// 
        /// </summary>
        public Order()
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// 
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Ascending
        {
            get { return _ascending; }
            set { _ascending = value; }
        }

        #endregion
    }
}