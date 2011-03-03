using System;

namespace Codefire.Storm.Mapping
{
    public interface IMapOverride
    {
        Type EntityType { get; }
        void Apply(EntityMap map, IMapStrategy strategy);
    }
}
