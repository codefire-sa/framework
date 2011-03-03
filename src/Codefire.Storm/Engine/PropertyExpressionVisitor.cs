using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Codefire.Storm.Engine
{
    public class PropertyExpressionVisitor : ExpressionVisitor
    {
        private StringBuilder _nameBuilder;
        private List<PropertyInfo> _properties;

        public PropertyExpressionVisitor()
            : base()
        {
            _nameBuilder = new StringBuilder();
            _properties = new List<PropertyInfo>();
        }

        public string Name
        {
            get { return _nameBuilder.ToString(); }
        }

        public PropertyInfo[] Properties
        {
            get { return _properties.ToArray(); }
        }

        public void Parse(Expression expression)
        {
            _nameBuilder.Length = 0;
            _properties.Clear();

            Visit(expression);
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            var result = base.VisitMemberAccess(m);

            if (_nameBuilder.Length > 0) _nameBuilder.Append(".");
            _nameBuilder.Append(m.Member.Name);

            _properties.Add(m.Member as PropertyInfo);

            return result;
        }
    }
}