using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codefire.Storm.Querying;
using Codefire.Storm.Engine;

namespace Codefire.Storm.Actions
{
    public class LoadAction<TEntity> : DatabaseAction<TEntity>
    {
        public LoadAction(IDataContext context)
            : base(context)
        {
        }

        public TEntity GetById(object id)
        {
            var template = CreateSelect();
            template.Criteria.Add(CriteriaType.And, Model.QueryPlan.Id.ColumnName, ComparisonOperator.Equals, id);

            var cmd = Context.Provider.Generate(template);

            var obj = default(TEntity);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    obj = (TEntity)Model.Instantiate();
                    Model.Hydrate(obj, reader);
                }
            }

            return obj;
        }

        public TEntity FindOne(EntityQueryValues qry)
        {
            var template = CreateSelect(qry);

            // Limit the result to one row.
            template.Limit = 1;

            var cmd = Context.Provider.Generate(template);

            var obj = default(TEntity);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    obj = (TEntity)Model.Instantiate();
                    Model.Hydrate(obj, reader);
                }
            }

            return obj;
        }

        public List<TEntity> FindMany(EntityQueryValues qry)
        {
            var template = CreateSelect(qry);

            var cmd = Context.Provider.Generate(template);

            var list = new List<TEntity>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var obj = (TEntity)Model.Instantiate();
                    Model.Hydrate(obj, reader);
                    list.Add(obj);
                }
            }

            return list;
        }
        
        protected QueryTemplate CreateSelect()
        {
            var template = new QueryTemplate(QueryType.Select);
            template.SelectColumns = Model.QueryPlan.BuildSelectColumns();
            template.TableName = Model.QueryPlan.TableName;
            template.TableAlias = Model.QueryPlan.TableAlias;
            foreach (var item in Model.QueryPlan.Joins)
            {
                var queryJoin = new Join();
                queryJoin.TableName = item.TableName;
                queryJoin.AliasName = item.TableAlias;
                queryJoin.LeftColumn = item.LeftColumnName;
                queryJoin.RightColumn = item.RightColumnName;
                queryJoin.JoinType = item.JoinType;
                template.Joins.Add(queryJoin);
            }

            return template;
        }

        protected QueryTemplate CreateSelect(EntityQueryValues qry)
        {
            var template = CreateSelect();

            template.Limit = qry.Limit;

            foreach (var criteriaItem in qry.Criteria)
            {
                criteriaItem.ColumnName = Model.QueryPlan.GetColumn(criteriaItem.ColumnName).ColumnName;
                template.Criteria.Add(criteriaItem);
            }

            foreach (var orderItem in qry.OrderBy)
            {
                orderItem.ColumnName = Model.QueryPlan.GetColumn(orderItem.ColumnName).ColumnName;
                template.OrderBy.Add(orderItem);
            }

            return template;
        }
    }
}