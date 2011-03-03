using System;
using System.Linq.Expressions;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Mapping
{
    public class JoinConfigurator<T>
    {
        private JoinMap _mapping;

        public JoinConfigurator(JoinMap mapping)
        {
            _mapping = mapping;
        }

        public void On(string parentColumn, string childColumn)
        {
            _mapping.ParentColumn = parentColumn;
            _mapping.ChildColumn = childColumn;
        }

        public PropertyConfigurator Property(Expression<Func<T, object>> expression)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            var memberName = visitor.Name;
            var columnName = memberName.Replace(".", "");

            var item = new PropertyMap();
            item.MemberName = memberName;
            item.Accessors = visitor.Properties;
            item.ColumnName = columnName;

            _mapping.Properties.Add(item);

            return new PropertyConfigurator(item);
        }
    }
}