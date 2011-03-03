using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Codefire.Storm.Querying
{
    public class Criteria
    {
        #region [ Fields ]

        private string _columnName;
        private CriteriaType _criteriaType;
        private ComparisonOperator _comparison;
        private object[] _valueList;

        #endregion

        public Criteria()
        {
        }

        public Criteria(ComparisonOperator comparison, object[] values)
        {
            _comparison = comparison;
            _valueList = values;
        }

        #region [ Properties ]

        /// <summary>
        /// 
        /// </summary>
        internal string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal CriteriaType CriteriaType
        {
            get { return _criteriaType; }
            set { _criteriaType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal ComparisonOperator Comparison
        {
            get { return _comparison; }
            set { _comparison = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal object[] ValueList
        {
            get { return _valueList; }
            set { _valueList = value; }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <returns></returns>
        public static Criteria Between(object startValue, object endValue)
        {
            var values = new object[] { startValue, endValue };
            return new Criteria(ComparisonOperator.Between, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsEqualTo(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.Equals, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsNotEqualTo(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.NotEquals, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsGreaterThan(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.GreaterThan, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsGreaterThanEqualTo(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.GreaterThanEquals, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsLessThan(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.LessThan, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria IsLessThanEqualTo(object value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.LessThanEquals, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Criteria IsNull()
        {
            return new Criteria(ComparisonOperator.IsNull, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Criteria IsNotNull()
        {
            return new Criteria(ComparisonOperator.IsNotNull, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Criteria In(IEnumerable values)
        {
            return new Criteria(ComparisonOperator.In, ConvertToArray(values));
        }

        private static object[] ConvertToArray(IEnumerable values)
        {
            var dataList = new ArrayList();
            foreach (var item in values)
            {
                dataList.Add(item);
            }
            
            return dataList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Criteria In(params object[] values)
        {
            return new Criteria(ComparisonOperator.In, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Criteria NotIn(IEnumerable values)
        {
            return new Criteria(ComparisonOperator.NotIn, ConvertToArray(values));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Criteria NotIn(params object[] values)
        {
            return new Criteria(ComparisonOperator.NotIn, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria Like(string value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.Like, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria StartsWith(string value)
        {
            var values = new object[] { value + "%" };
            return new Criteria(ComparisonOperator.Like, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria EndsWith(string value)
        {
            var values = new object[] { "%" + value };
            return new Criteria(ComparisonOperator.Like, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria Contains(string value)
        {
            var values = new object[] { "%" + value + "%" };
            return new Criteria(ComparisonOperator.Like, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Criteria NotLike(string value)
        {
            var values = new object[] { value };
            return new Criteria(ComparisonOperator.NotLike, values);
        }

        #endregion
    }
}