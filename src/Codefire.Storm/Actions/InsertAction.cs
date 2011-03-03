using System;
using System.Data;
using Codefire.Storm.Engine;
using Codefire.Storm.Mapping;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Actions
{
    public class InsertAction<TEntity> : DatabaseAction<TEntity>
    {
        public InsertAction(IDataContext context)
            : base(context)
        {
        }

        public object Insert(TEntity entity)
        {
            object id = null;

            Context.Interceptor.OnInserting(entity, Model, Context.CurrentUser);

            if (string.IsNullOrEmpty(Model.Map.InsertProcedure))
            {
                id = InsertUsingSql(entity);
            }
            else
            {
                id = InsertUsingProcedure(entity);
            }

            Context.Interceptor.OnInserted(entity, Model, Context.CurrentUser);

            return id;
        }

        private object InsertUsingSql(TEntity entity)
        {
            var insertQuery = new QueryTemplate(QueryType.Insert);
            insertQuery.TableName = Model.Map.TableName;

            foreach (var propertyItem in Model.Map.Properties)
            {
                if ((propertyItem.Options & ColumnOptions.Insert) != ColumnOptions.Insert) continue;

                var value = Model.GetValue(entity, propertyItem.Accessors);
                insertQuery.InsertValues.Add(propertyItem.ColumnName, value);
            }

            insertQuery.SelectIdentity = true;

            var cmd = Context.Provider.Generate(insertQuery);
            var id = cmd.ExecuteScalar();

            Model.SetId(entity, id);

            return id;
        }

        private object InsertUsingProcedure(TEntity entity)
        {
            var cmd = Context.CreateCommand(Model.Map.InsertProcedure, CommandType.StoredProcedure);

            var specialOptions = ColumnOptions.CreateUser | ColumnOptions.CreateDate | ColumnOptions.ModifyUser | ColumnOptions.ModifyDate | ColumnOptions.SoftDelete;

            foreach (var propertyItem in Model.Map.Properties)
            {
                if ((propertyItem.Options & ColumnOptions.Insert) == ColumnOptions.Insert) continue;
                if ((propertyItem.Options & specialOptions) != ColumnOptions.None) continue;

                var paramName = Context.Provider.GetParameterName(propertyItem.ColumnName);
                var value = Model.GetValue(entity, propertyItem.Accessors);
                cmd.AddParameter(paramName, value);
            }

            var userParam = Context.Provider.GetParameterName(Model.Map.CurrentUserParameter);
            cmd.AddParameter(userParam, Context.CurrentUser);
            var id = cmd.ExecuteScalar<object>();

            Model.SetId(entity, id);

            return id;
        }
    }
}