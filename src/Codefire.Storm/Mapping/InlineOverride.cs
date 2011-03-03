using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codefire.Storm.Mapping
{
    public class InlineOverride<TEntity> : IMapOverride
    {
        private Action<EntityConfigurator<TEntity>> _action;

        public InlineOverride(Action<EntityConfigurator<TEntity>> action)
        {
            _action = action;
        }

        public Type EntityType
        {
            get { return typeof(TEntity); }
        }

        public void Apply(EntityMap map, IMapStrategy strategy)
        {
            var configurator = new EntityConfigurator<TEntity>(map, strategy);

            _action(configurator);
        }
    }
}
