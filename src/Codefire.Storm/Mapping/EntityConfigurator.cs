using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Codefire.Storm.Engine;
using System.Reflection;

namespace Codefire.Storm.Mapping
{
    public class EntityConfigurator<T>
    {
        private EntityMap _map;
        private IMapStrategy _strategy;

        public EntityConfigurator(EntityMap map, IMapStrategy strategy)
        {
            _map = map;
            _strategy = strategy;
        }

        public IdConfigurator Id(Expression<Func<T, object>> expression)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            var memberName = visitor.Name;
            var columnName = _strategy.GetIdName(_map.EntityType, memberName);

            var item = _strategy.CreateIdMap(memberName, columnName, visitor.Properties);
            _map.Id = item;

            return new IdConfigurator(item);
        }

        public PropertyConfigurator Property(Expression<Func<T, object>> expression)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            var memberName = visitor.Name;
            var item = _map.Properties.Find(x => x.MemberName == memberName);
            if (item == null)
            {
                var columnName = _strategy.GetColumnName(_map.EntityType, memberName);
                item = _strategy.CreatePropertyMap(memberName, columnName, visitor.Properties);
                _map.Properties.Add(item);
            }

            return new PropertyConfigurator(item);
        }

        public void Ignore(Expression<Func<T, object>> expression)
        {
            var visitor = new PropertyExpressionVisitor();
            visitor.Parse(expression);

            var memberName = visitor.Name;
            _map.Properties.RemoveAll(x => x.MemberName == memberName);
        }

        public void Join(string tableName, Action<JoinConfigurator<T>> action)
        {
            var item = new JoinMap();
            item.TableName = tableName;

            _map.Joins.Add(item);

            var part = new JoinConfigurator<T>(item);
            action(part);
        }

        public void Table(string tableName)
        {
            _map.TableName = tableName;
        }

        public void View(string viewName)
        {
            _map.ViewName = viewName;
        }

        public void UseStoredProcedures(string insertProcedure, string updateProcedure, string deleteProcedure, string reinstateProcedure)
        {
            _map.InsertProcedure = insertProcedure;
            _map.UpdateProcedure = updateProcedure;
            _map.DeleteProcedure = deleteProcedure;
            _map.ReinstateProcedure = reinstateProcedure;
        }

        public void CurrentUserParameter(string parameterName)
        {
            _map.CurrentUserParameter = parameterName;
        }

        public void ReadOnly()
        {
            _map.ReadOnly = true;
        }
    }
}