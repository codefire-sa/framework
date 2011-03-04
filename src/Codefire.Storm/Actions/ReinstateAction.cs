using System;
using System.Data;
using Codefire.Storm.Engine;
using Codefire.Storm.Mapping;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Actions
{
    public class ReinstateAction<TEntity> : DatabaseAction<TEntity>
    {
        public ReinstateAction(IDataContext context)
            : base(context)
        {
        }

        public void Reinstate(object id)
        {
            Context.Interceptor.OnReinstating(id, Model, Context.CurrentUser);

            if (string.IsNullOrEmpty(Model.Map.ReinstateProcedure))
            {
                ReinstateUsingSql(id);
            }
            else
            {
                ReinstateUsingProcedure(id);
            }

            Context.Interceptor.OnReinstated(id, Model, Context.CurrentUser);
        }

        private void ReinstateUsingSql(object id)
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
                    updateQuery.UpdateValues.Add(propertyItem.ColumnName, true);
                }
            }

            updateQuery.Criteria.Add(CriteriaType.And, null, Model.Map.Id.ColumnName, ComparisonOperator.Equals, id);

            var cmd = Context.Provider.Generate(updateQuery);
            cmd.ExecuteNonQuery();
        }

        private void ReinstateUsingProcedure(object id)
        {
            var idParam = Context.Provider.GetParameterName(Model.Map.Id.ColumnName);
            var userParam = Context.Provider.GetParameterName(Model.Map.CurrentUserParameter);

            var cmd = Context.CreateCommand(Model.Map.ReinstateProcedure, CommandType.StoredProcedure);
            cmd.AddParameter(idParam, id);
            cmd.AddParameter(userParam, Context.CurrentUser);
            cmd.ExecuteNonQuery();
        }
    }
}