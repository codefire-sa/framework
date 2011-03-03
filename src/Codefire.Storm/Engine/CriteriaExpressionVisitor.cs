using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Codefire.Storm.Querying;
using System.Collections;
using System.Text;

namespace Codefire.Storm.Engine
{
    public class CriteriaExpressionVisitor : ExpressionVisitor
    {
        private CriteriaCollection _criteriaList;
        private Queue<CriteriaType> _criteriaTypeQueue;
        private StringBuilder _nameBuilder;
        private object _value;
        private bool _isLeft;

        public CriteriaExpressionVisitor()
        {
            _criteriaList = new CriteriaCollection();
            _criteriaTypeQueue = new Queue<CriteriaType>();
            _nameBuilder = new StringBuilder();
        }

        public CriteriaCollection Criteria
        {
            get { return _criteriaList; }
        }

        public void Parse(Expression expression)
        {
            Parse(expression, CriteriaType.None);
        }

        public void Parse(Expression expression, CriteriaType criteriaType)
        {
            _criteriaList.Clear();
            _criteriaTypeQueue.Clear();
            _criteriaTypeQueue.Enqueue(criteriaType);

            Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.AndAlso:
                    return ProcessLogical(b, CriteriaType.And);
                case ExpressionType.OrElse:
                    return ProcessLogical(b, CriteriaType.Or);
                default:
                    return ProcessBinary(b);
            }
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            _value = c.Value;
           
            return base.VisitConstant(c);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (_isLeft)
            {
                base.VisitMemberAccess(m);

                if (_nameBuilder.Length > 0) _nameBuilder.Append(".");
                _nameBuilder.Append(m.Member.Name);
            }
            else
            {
                var e = Evaluate(m) as ConstantExpression;
                _value = e.Value;
            }

            return m;
        }

        private Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }
        
            var lambda = Expression.Lambda(e);
            var fn = lambda.Compile();
            
            return Expression.Constant(fn.DynamicInvoke(null), e.Type);
        }

        private Expression ProcessLogical(BinaryExpression b, CriteriaType criteriaType)
        {
            _criteriaTypeQueue.Enqueue(criteriaType);

            return base.VisitBinary(b);
        }

        private Expression ProcessBinary(BinaryExpression b)
        {
            _nameBuilder.Length = 0;
            _value = null;

            _isLeft = true;
            base.Visit(b.Left);

            _isLeft = false;
            base.Visit(b.Right);

            var comparison = ComparisonOperator.None;
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                    comparison = _value == null ? ComparisonOperator.IsNull : ComparisonOperator.Equals;
                    break;
                case ExpressionType.NotEqual:
                    comparison = _value == null ? ComparisonOperator.IsNotNull : ComparisonOperator.NotEquals;
                    break;
                case ExpressionType.LessThan:
                    comparison = ComparisonOperator.LessThan;
                    break;
                case ExpressionType.LessThanOrEqual:
                    comparison = ComparisonOperator.LessThanEquals;
                    break;
                case ExpressionType.GreaterThan:
                    comparison = ComparisonOperator.GreaterThan;
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    comparison = ComparisonOperator.GreaterThanEquals;
                    break;
            }

            if (comparison != ComparisonOperator.None)
            {
                AddCriteria(_nameBuilder.ToString(), comparison, new object[] { _value });
            }

            return b;
        }

        private void AddCriteria(string columnName, ComparisonOperator comparison, object[] values)
        {
            var criteriaType = _criteriaTypeQueue.Dequeue();
            _criteriaList.Add(criteriaType, columnName, comparison, values);
        }
    }
}