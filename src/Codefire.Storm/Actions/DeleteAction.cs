using System;
using System.Data;
using Codefire.Storm.Engine;
using Codefire.Storm.Mapping;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Actions
{
    public class DeleteAction<TEntity> : DatabaseAction<TEntity>
    {
        public DeleteAction(IDataContext context)
            : base(context)
        {
        }

        public void Delete(object id)
        {
            Context.Interceptor.OnDeleting(id, Model, Context.CurrentUser);

            if (string.IsNullOrEmpty(Model.Map.DeleteProcedure))
            {
                DeleteUsingSql(id);
            }
            else
            {
                DeleteUsingProcedure(id);
            }

            Context.Interceptor.OnDeleted(id, Model, Context.CurrentUser);
        }

        private void DeleteUsingSql(object id)
        {
            var updateQuery = new QueryTemplate(QueryType.Update);
            updateQuery.TableName = Model.Map.TableName;

            foreach (var propertyItem in Model.Map.Properties)
            {
                if ((propertyItem.Options & ColumnOptions.ModifyUser) == ColumnOptions.ModifyUser)
                {
                    updateQuery.UpdateValues.Add(propertyItem.ColumnName, Context.CurrentUser);
                }
                else if ((propertyItem.Options & ColumnOptions.ModifyDate) == ColumnOptions.ModifyDate)
                {
                    updateQuery.UpdateValues.Add(propertyItem.ColumnName, DateTime.Now);
                }
                else if ((propertyItem.Options & ColumnOptions.SoftDelete) == ColumnOptions.SoftDelete)
                {
                    updateQuery.UpdateValues.Add(propertyItem.ColumnName, false);
                }
            }

            updateQuery.Criteria.Add(CriteriaType.And, null, Model.Map.Id.ColumnName, ComparisonOperator.Equals, id);

            var cmd = Context.Provider.Generate(updateQuery);
            cmd.ExecuteNonQuery();
        }

        private void DeleteUsingProcedure(object id)
        {
            var idParam = Context.Provider.GetParameterName(Model.Map.Id.ColumnName);
            var userParam = Context.Provider.GetParameterName(Model.Map.CurrentUserParameter);

            var cmd = Context.CreateCommand(Model.Map.DeleteProcedure, CommandType.StoredProcedure);
            cmd.AddParameter(idParam, id);
            cmd.AddParameter(userParam, Context.CurrentUser);
            cmd.ExecuteNonQuery();
        }
    }
}