using System;
using System.Reflection;
using Codefire.Storm.Mapping;

namespace Codefire.Storm
{
    public interface IMapStrategy
    {
        bool ShouldMap(Type type);
        bool ShouldMap(PropertyInfo member);
        bool IsId(PropertyInfo member);
        bool IsComponent(PropertyInfo member);

        string GetTableName(Type classType);
        string GetIdName(Type classType, string memberName);
        string GetColumnName(Type classType, string memberName);

        EntityMap CreateEntityMap(Type type);
        IdMap CreateIdMap(string memberName, string columnName, PropertyInfo[] accessors);
        PropertyMap CreatePropertyMap(string memberName, string columnName, PropertyInfo[] accessors);

        void MapComponent(PropertyInfo member, EntityMap map);
    }
}
