using System;
using System.Collections.Generic;
using Codefire.Storm.Mapping;

namespace Codefire.Storm.Engine
{
    public class EntityContainer
    {
        private Dictionary<Type, EntityModel> _models;

        public EntityContainer()
        {
            _models = new Dictionary<Type, EntityModel>();
        }

        public void Add(Type entityType, EntityModel model)
        {
            _models[entityType] = model;
        }

        public EntityModel Get(Type entityType)
        {
            EntityModel model = null;

            _models.TryGetValue(entityType, out model);

            return model;
        }
    }
}