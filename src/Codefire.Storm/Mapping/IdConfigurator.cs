using System;

namespace Codefire.Storm.Mapping
{
    public class IdConfigurator
    {
        private IdMap _mapping;

        public IdConfigurator(IdMap mapping)
        {
            _mapping = mapping;
        }

        public IdConfigurator Column(string value)
        {
            _mapping.ColumnName = value;
            return this;
        }

        public IdConfigurator Assigned()
        {
            _mapping.IsAutoIncrement = false;
            return this;
        }

        public IdConfigurator AutoIncrement()
        {
            _mapping.IsAutoIncrement = true;
            return this;
        }
    }
}