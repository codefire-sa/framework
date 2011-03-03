using System;

namespace Codefire.Storm.Mapping
{
    public abstract class EntityMapOverride<TEntity> : IMapOverride
    {
        public EntityMapOverride()
        {
        }

        public Type EntityType
        {
            get { return typeof(TEntity); }
        }

        public void Apply(EntityMap map, IMapStrategy strategy)
        {
            var configurator = new EntityConfigurator<TEntity>(map, strategy);

            OnOverride(configurator);
        }

        protected abstract void OnOverride(EntityConfigurator<TEntity> configurator);
    }
}