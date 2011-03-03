using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codefire.Storm.Engine;

namespace Codefire.Storm
{
    public static class StormContainer
    {
        private static Dictionary<Type, EntityContainer> _containers;

        static StormContainer()
        {
            _containers = new Dictionary<Type, EntityContainer>();
        }

        public static void Register<TContext>() where TContext : IContextInitializer, new()
        {
            var context = new TContext();
            var container = context.BuildContainer();

            _containers.Add(context.GetType(), container);
        }

        public static EntityContainer Get(Type contextType)
        {
            EntityContainer container;
            _containers.TryGetValue(contextType, out container);

            return container;
        }
    }
}