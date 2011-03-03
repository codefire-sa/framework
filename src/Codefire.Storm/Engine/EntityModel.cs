using System;
using System.Data;
using System.Linq;
using System.Reflection;
using Codefire.Storm.Actions;
using Codefire.Storm.Mapping;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Engine
{
    public class EntityModel
    {
        private EntityMap _map;
        private QueryPlan _queryPlan;

        public EntityModel(EntityMap map)
        {
            _map = map;
            _queryPlan = BuildQueryPlan();
        }

        public EntityMap Map
        {
            get { return _map; }
        }

        public QueryPlan QueryPlan
        {
            get { return _queryPlan; }
        }

        public object GetId(object entity)
        {
            return GetValue(entity, _map.Id.Accessors);
        }

        public void SetId(object entity, object id)
        {
            SetValue(entity, _map.Id.Accessors, id);
        }

        public object Instantiate()
        {
            return Activator.CreateInstance(_map.EntityType);
        }

        public void Hydrate(object entity, IDataReader reader)
        {
            foreach (var columnItem in _queryPlan.Columns)
            {
                SetValue(entity, columnItem.Accessors, reader[columnItem.ColumnAlias]);
            }
        }

        public object GetValue(object entity, PropertyInfo[] accessorList)
        {
            var obj = entity;
            for (int x = 0; x < accessorList.Length - 1; x++)
            {
                obj = accessorList[x].GetValue(obj, null);
            }

            return accessorList.Last().GetValue(obj, null);
        }

        public void SetValue(object entity, PropertyInfo[] accessorList, object propertyValue)
        {
            if (propertyValue == DBNull.Value) return;

            var obj = entity;
            for (int x = 0; x < accessorList.Length - 1; x++)
            {
                obj = accessorList[x].GetValue(obj, null);
            }

            var accessor = accessorList.Last();
            if (accessor.PropertyType.IsEnum)
            {
                propertyValue = Enum.ToObject(accessor.PropertyType, propertyValue);
            }
            else if (!accessor.PropertyType.IsAssignableFrom(propertyValue.GetType()))
            {
                propertyValue = Convert.ChangeType(propertyValue, accessor.PropertyType);
            }
            accessor.SetValue(obj, propertyValue, null);
        }

        private QueryPlan BuildQueryPlan()
        {
            var plan = new QueryPlan();
            int count = 0;

            var tableAlias = "t" + count.ToString();
            if (string.IsNullOrEmpty(_map.ViewName))
            {
                plan.TableName = _map.TableName;
            }
            else
            {
                plan.TableName = _map.ViewName;
            }
            plan.TableAlias = tableAlias;

            var idName = tableAlias + "." + _map.Id.ColumnName;
            var idAlias = tableAlias + "_" + _map.Id.ColumnName;
            plan.AddId(_map.Id.MemberName, idName, idAlias, _map.Id.Accessors);

            foreach (var propertyItem in _map.Properties)
            {
                var columnName = tableAlias + "." + propertyItem.ColumnName;
                var columnAlias = tableAlias + "_" + propertyItem.ColumnName;
                plan.AddColumn(propertyItem.MemberName, columnName, columnAlias, propertyItem.Accessors);
            }

            foreach (var lookupItem in _map.Joins)
            {
                count++;
                var lookupAlias = "t" + count.ToString();

                var leftColumnName = tableAlias + "." + lookupItem.ParentColumn;
                var rightColumnName = lookupAlias + "." + lookupItem.ChildColumn;
                plan.AddJoin(lookupItem.TableName, lookupAlias, leftColumnName, rightColumnName, JoinType.LeftOuter);

                foreach (var propertyItem in lookupItem.Properties)
                {
                    var columnName = lookupAlias + "." + propertyItem.ColumnName;
                    var columnAlias = lookupAlias + "_" + propertyItem.ColumnName;
                    plan.AddColumn(propertyItem.MemberName, columnName, columnAlias, propertyItem.Accessors);
                }
            }

            return plan;
        }
    }
}
