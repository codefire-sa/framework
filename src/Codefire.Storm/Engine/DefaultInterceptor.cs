using System;
using Codefire.Storm.Mapping;

namespace Codefire.Storm.Engine
{
    public class DefaultInterceptor : IInterceptor
    {
        public virtual void OnLoading(object entity, EntityModel model, string[] propertyNames, object[] values, object currentUser)
        {
        }

        public virtual void OnLoaded(object entity, EntityModel model, object currentUser)
        {
        }

        public virtual void OnInserting(object entity, EntityModel model, object currentUser)
        {
            var currentDate = DateTime.Now;
            var userOptions = ColumnOptions.CreateUser | ColumnOptions.ModifyUser;
            var dateOptions = ColumnOptions.CreateDate | ColumnOptions.ModifyDate;

            foreach (var propertyItem in model.Map.Properties)
            {
                if ((propertyItem.Options & userOptions) != ColumnOptions.None)
                {
                    model.SetValue(entity, propertyItem.Accessors, currentUser);
                }
                else if ((propertyItem.Options & dateOptions) != ColumnOptions.None)
                {
                    model.SetValue(entity, propertyItem.Accessors, currentDate);
                }
                else if ((propertyItem.Options & ColumnOptions.SoftDelete) == ColumnOptions.SoftDelete)
                {
                    model.SetValue(entity, propertyItem.Accessors, true);
                }
            }
        }

        public virtual void OnInserted(object entity, EntityModel model, object currentUser)
        {
        }

        public virtual void OnUpdating(object entity, EntityModel model, object currentUser)
        {
            var userOptions = ColumnOptions.ModifyUser;
            var dateOptions = ColumnOptions.ModifyDate;
            foreach (var propertyItem in model.Map.Properties)
            {
                if ((propertyItem.Options & userOptions) != ColumnOptions.None)
                {
                    model.SetValue(entity, propertyItem.Accessors, currentUser);
                }
                else if ((propertyItem.Options & dateOptions) != ColumnOptions.None)
                {
                    model.SetValue(entity, propertyItem.Accessors, DateTime.Now);
                }
            }
        }

        public virtual void OnUpdated(object entity, EntityModel model, object currentUser)
        {
        }

        public virtual void OnDeleting(object id, EntityModel model, object currentUser)
        {
        }

        public virtual void OnDeleted(object id, EntityModel model, object currentUser)
        {
        }

        public virtual void OnReinstating(object id, EntityModel model, object currentUser)
        {
        }

        public virtual void OnReinstated(object id, EntityModel model, object currentUser)
        {
        }
    }
}