using System;
using System.Collections.Generic;
using System.Reflection;
using Codefire.Storm.Engine;
using Codefire.Storm.Mapping;

namespace Codefire.Storm
{
    public class ModelBuilder
    {
        private IMapStrategy _strategy;
        private List<Assembly> _entityAssemblies;
        private List<Type> _entityTypes;
        private Dictionary<Type, IMapOverride> _overrides;

        public ModelBuilder()
        {
            _entityAssemblies = new List<Assembly>();
            _entityTypes = new List<Type>();
            _overrides = new Dictionary<Type, IMapOverride>();
            _strategy = new DefaultStrategy();
        }

        public IMapStrategy Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        public void AddAssembly(string name)
        {
            var assembly = Assembly.Load(name);
            AddAssembly(assembly);
        }

        public void AddAssembly(Assembly assembly)
        {
            _entityAssemblies.Add(assembly);
        }

        public void AddAssemblyOf<T>()
        {
            AddAssembly(typeof(T).Assembly);
        }

        public void AddType<TEntity>()
        {
            AddType(typeof(TEntity));
        }

        public void AddType(Type entityType)
        {
            _entityTypes.Add(entityType);
        }

        public void Override<TEntity>(Action<EntityConfigurator<TEntity>> action)
        {
            var overrideObj = new InlineOverride<TEntity>(action);

            _overrides.Add(overrideObj.EntityType, overrideObj);
        }

        public void UseOverridesFromAssembly(Assembly assembly)
        {
            var typeList = assembly.GetExportedTypes();
            foreach (var typeItem in typeList)
            {
                if (typeItem.IsAbstract) continue;
                if (typeItem.IsInterface) continue;

                if (IsOverrideClass(typeItem))
                {
                    var overrideObj = Activator.CreateInstance(typeItem) as IMapOverride;
                    if (overrideObj != null)
                    {
                        _overrides.Add(overrideObj.EntityType, overrideObj);
                    }
                }
            }
        }

        public void UseOverridesFromAssemblyOf<T>()
        {
            UseOverridesFromAssembly(typeof(T).Assembly);
        }

        public EntityContainer Build()
        {
            var container = new EntityContainer();

            BuildAssemblies(container, _entityAssemblies);
            BuildTypes(container, _entityTypes);

            return container;
        }

        private void BuildAssemblies(EntityContainer container, IEnumerable<Assembly> assemblyList)
        {
            foreach (var assemblyItem in assemblyList)
            {
                var typeList = assemblyItem.GetExportedTypes();
                BuildTypes(container, typeList);
            }
        }

        private void BuildTypes(EntityContainer container, IEnumerable<Type> typeList)
        {
            foreach (var typeItem in typeList)
            {
                var map = CreateEntityMap(typeItem);

                if (map != null)
                {
                    var model = new EntityModel(map);
                    container.Add(typeItem, model);
                }
            }
        }

        private EntityMap CreateEntityMap(Type entityType)
        {
            if (!_strategy.ShouldMap(entityType)) return null;

            var map = _strategy.CreateEntityMap(entityType);
            map.TableName = _strategy.GetTableName(entityType);

            var propertyList = entityType.GetProperties();
            foreach (var propertyItem in propertyList)
            {
                if (!_strategy.ShouldMap(propertyItem)) continue;

                if (_strategy.IsId(propertyItem))
                {
                    map.Id = CreateIdMap(entityType, propertyItem);
                }
                else if (_strategy.IsComponent(propertyItem))
                {
                    _strategy.MapComponent(propertyItem, map);
                }
                else
                {
                    map.Properties.Add(CreatePropertyMap(entityType, propertyItem));
                }
            }

            if (_overrides.ContainsKey(entityType))
            {
                var overrideObj = _overrides[entityType];
                overrideObj.Apply(map, _strategy);
            }

            return map;
        }

        private IdMap CreateIdMap(Type entityType, PropertyInfo member)
        {
            var columnName = _strategy.GetIdName(entityType, member.Name);

            return _strategy.CreateIdMap(member.Name, columnName, new PropertyInfo[] { member });
        }

        private PropertyMap CreatePropertyMap(Type entityType, PropertyInfo member)
        {
            var columnName = _strategy.GetColumnName(entityType, member.Name);

            return _strategy.CreatePropertyMap(member.Name, columnName, new PropertyInfo[] { member });
        }

        private bool IsOverrideClass(Type checkType)
        {
            var genericType = typeof(EntityMapOverride<>);

            while (checkType != typeof(object))
            {
                if (checkType.IsGenericType && checkType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }

                checkType = checkType.BaseType;
            }
            return false;
        }
    }
}