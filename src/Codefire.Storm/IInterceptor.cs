using System;
using Codefire.Storm.Engine;

namespace Codefire.Storm
{
    public interface IInterceptor
    {
        void OnLoading(object entity, EntityModel model, string[] propertyNames, object[] values, object currentUser);
        void OnLoaded(object entity, EntityModel model, object currentUser);
        void OnInserting(object entity, EntityModel model, object currentUser);
        void OnInserted(object entity, EntityModel model, object currentUser);
        void OnUpdating(object entity, EntityModel model, object currentUser);
        void OnUpdated(object entity, EntityModel model, object currentUser);
        void OnDeleting(object id, EntityModel model, object currentUser);
        void OnDeleted(object id, EntityModel model, object currentUser);
        void OnReinstating(object id, EntityModel model, object currentUser);
        void OnReinstated(object id, EntityModel model, object currentUser);
    }
}