using System;
using System.Data;
using Codefire.Storm.Engine;
using Codefire.Storm.Mapping;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Actions
{
    public class UpdateAction<TEntity> : DatabaseAction<TEntity>
    {
        public UpdateAction(IDataContext context)
            : base(context)
        {
        }

        public void Update(TEntity entity)
        {
            Context.Interceptor.OnUpdating(entity, Model, Context.CurrentUser);

            if (string.IsNullOrEmpty(Model.Map.UpdateProcedure))
            {
                UpdateUsingSql(entity);
            }
            else
            {
                UpdateUsingProcedure(entity);
            }

            Context.Interceptor.OnUpdated(entity, Model, Context.CurrentUser);
        }

        private void UpdateUsingSql(TEntity entity)
        {
            var updateQuery = new QueryTemplate(QueryType.Update);
            updateQuery.TableName = Model.Map.TableName;

            var ignoreOptions = ColumnOptions.CreateUser | ColumnOptions.CreateDate | ColumnOptions.SoftDelete;
            foreach (var propertyItem in Model.Map.Properties)
            {
                if ((propertyItem.Options & ColumnOptions.Update) != ColumnOptions.Update) continue;
                if ((propertyItem.Options & ignoreOptions) != ColumnOptions.None) continue;

                var value = Model.GetValue(entity, propertyItem.Accessors);
                updateQuery.UpdateValues.Add(propertyItem.ColumnName, value);
            }

            var idValue = Model.GetId(entity);
            updateQuery.Criteria.Add(CriteriaType.And, null, Model.Map.Id.ColumnName, ComparisonOperator.Equals, idValue);

            var cmd = Context.Provider.Generate(updateQuery);
            cmd.ExecuteNonQuery();
        }

        private void UpdateUsingProcedure(TEntity entity)
        {
            var cmd = Context.CreateCommand(Model.Map.UpdateProcedure, CommandType.StoredProcedure);

            var idParam = Context.Provider.GetParameterName(Model.Map.Id.ColumnName);
            var idValue = Model.GetValue(entity, Model.Map.Id.Accessors);
            cmd.AddParameter(idParam, idValue);

            var specialOptions = ColumnOptions.CreateUser | ColumnOptions.CreateDate | ColumnOptions.ModifyUser | ColumnOptions.ModifyDate | ColumnOptions.SoftDelete;

            foreach (var propertyItem in Model.Map.Properties)
            {
                if ((propertyItem.Options & ColumnOptions.Update) != ColumnOptions.Update) continue;
                if ((propertyItem.Options & specialOptions) != ColumnOptions.None) continue;

                var paramName = Context.Provider.GetParameterName(propertyItem.ColumnName);
                var value = Model.GetValue(entity, propertyItem.Accessors);
                cmd.AddParameter(paramName, value);
            }

            var userParam = Context.Provider.GetParameterName(Model.Map.CurrentUserParameter);
            cmd.AddParameter(userParam, Context.CurrentUser);
            cmd.ExecuteNonQuery();
        }
    }
}