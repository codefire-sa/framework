using System;

namespace Codefire.Storm.Mapping
{
    public class PropertyConfigurator
    {
        private PropertyMap _mapping;

        public PropertyConfigurator(PropertyMap mapping)
        {
            _mapping = mapping;
        }

        public PropertyConfigurator Column(string value)
        {
            _mapping.ColumnName = value;
            return this;
        }

        public PropertyConfigurator InsertOnly()
        {
            _mapping.Options |= ColumnOptions.Insert;
            _mapping.Options &= ~ColumnOptions.Update;
            return this;
        }

        public PropertyConfigurator UpdateOnly()
        {
            _mapping.Options &= ~ColumnOptions.Insert;
            _mapping.Options |= ColumnOptions.Update;
            return this;
        }

        public PropertyConfigurator ReadOnly()
        {
            _mapping.Options &= ~ColumnOptions.Insert;
            _mapping.Options &= ~ColumnOptions.Update;
            return this;
        }

        public PropertyConfigurator CreateUser()
        {
            _mapping.Options |= ColumnOptions.CreateUser;
            return this;
        }

        public PropertyConfigurator CreateDate()
        {
            _mapping.Options |= ColumnOptions.CreateDate;
            return this;
        }

        public PropertyConfigurator ModifyUser()
        {
            _mapping.Options |= ColumnOptions.ModifyUser;
            return this;
        }

        public PropertyConfigurator ModifyDate()
        {
            _mapping.Options |= ColumnOptions.ModifyDate;
            return this;
        }

        public PropertyConfigurator SoftDelete()
        {
            _mapping.Options |= ColumnOptions.SoftDelete;
            return this;
        }
    }
}