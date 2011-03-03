using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codefire.Storm.Mapping;

namespace Codefire.Storm.Engine
{
    public class DefaultStrategy : IMapStrategy
    {
        #region [ Fields ]

        private string _createUserMember;
        private string _createDateMember;
        private string _modifyUserMember;
        private string _modifyDateMember;
        private string _softDeleteMember;
        private string _currentUserParameter;
        private List<string> _ignoreMembers;

        #endregion
        
        public DefaultStrategy()
        {
            _createUserMember = "CreateUserId";
            _createDateMember = "CreateDate";
            _modifyUserMember = "ModifyUserId";
            _modifyDateMember = "ModifyDate";
            _softDeleteMember = "IsActive";
            _currentUserParameter = "CurrentUser";
            _ignoreMembers = new List<string>();
        }

        public string CreateUserMember
        {
            get { return _createUserMember; }
            set { _createUserMember = value; }
        }

        public string CreateDateMember
        {
            get { return _createDateMember; }
            set { _createDateMember = value; }
        }

        public string ModifyUserMember
        {
            get { return _modifyUserMember; }
            set { _modifyUserMember = value; }
        }

        public string ModifyDateMember
        {
            get { return _modifyDateMember; }
            set { _modifyDateMember = value; }
        }

        public string SoftDeleteMember
        {
            get { return _softDeleteMember; }
            set { _softDeleteMember = value; }
        }

        public string CurrentUserParameter
        {
            get { return _currentUserParameter; }
            set { _currentUserParameter = value; }
        }

        public List<string> IgnoreMembers
        {
            get { return _ignoreMembers; }
        }

        public virtual bool ShouldMap(Type type)
        {
            if (type.IsAbstract) return false;

            return type.Name.EndsWith("Entity");
        }

        public virtual bool ShouldMap(PropertyInfo member)
        {
            if (!member.CanWrite) return false;
            if (_ignoreMembers.Exists(x => x == member.Name)) return false;

            return true;
        }

        public virtual bool IsId(PropertyInfo member)
        {
            return member.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool IsComponent(PropertyInfo member)
        {
            if (member.PropertyType.IsValueType) return false;
            if (member.PropertyType == typeof(string)) return false;

            return true;
        }

        public virtual string GetTableName(Type classType)
        {
            var className = classType.Name;
            if (className.EndsWith("Entity"))
            {
                className = className.Replace("Entity", "");
            }

            var tableName = Inflector.Default.MakePlural(className);

            return "dbo." + tableName;
        }

        public virtual string GetIdName(Type classType, string memberName)
        {
            return "Id";
        }

        public virtual string GetColumnName(Type classType, string memberName)
        {
            return memberName.Replace(".", "");
        }

        public virtual EntityMap CreateEntityMap(Type type)
        {
            var map = new EntityMap();
            map.EntityType = type;
            map.CurrentUserParameter = _currentUserParameter;

            return map;
        }

        public virtual IdMap CreateIdMap(string memberName, string columnName, PropertyInfo[] accessors)
        {
            var item = new IdMap();
            item.MemberName = memberName;
            item.ColumnName = columnName;
            item.Accessors = accessors;
            item.IsAutoIncrement = true;

            return item;
        }

        public virtual PropertyMap CreatePropertyMap(string memberName, string columnName, PropertyInfo[] accessors)
        {
            var item = new PropertyMap();
            item.MemberName = memberName;
            item.ColumnName = columnName;
            item.Accessors = accessors;
            item.Options = ColumnOptions.Insert | ColumnOptions.Update;

            if (memberName.Equals(_createUserMember, StringComparison.InvariantCultureIgnoreCase)) item.Options |= ColumnOptions.CreateUser;
            if (memberName.Equals(_createDateMember, StringComparison.InvariantCultureIgnoreCase)) item.Options |= ColumnOptions.CreateDate;
            if (memberName.Equals(_modifyUserMember, StringComparison.InvariantCultureIgnoreCase)) item.Options |= ColumnOptions.ModifyUser;
            if (memberName.Equals(_modifyDateMember, StringComparison.InvariantCultureIgnoreCase)) item.Options |= ColumnOptions.ModifyDate;
            if (memberName.Equals(_softDeleteMember, StringComparison.InvariantCultureIgnoreCase)) item.Options |= ColumnOptions.SoftDelete;

            return item;
        }

        public virtual void MapComponent(PropertyInfo member, EntityMap map)
        {
        }
    }
}