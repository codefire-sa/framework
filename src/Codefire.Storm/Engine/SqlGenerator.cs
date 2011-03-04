using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Codefire.Storm.Querying;

namespace Codefire.Storm.Engine
{
    public class SqlGenerator
    {
        private IDataProvider _provider;
        private int _parameterCount;

        public SqlGenerator(IDataProvider provider)
        {
            _provider = provider;
        }

        public IDataCommand Build(QueryTemplate template)
        {
            switch (template.QueryType)
            {
                case QueryType.Select:
                    return BuildSelect(template);
                case QueryType.Insert:
                    return BuildInsert(template);
                case QueryType.Update:
                    return BuildUpdate(template);
                case QueryType.Delete:
                    return BuildDelete(template);
                default:
                    return null;
            }
        }

        private IDataCommand BuildSelect(QueryTemplate template)
        {
            IDataCommand cmd = null;

            if (template.AggregateType == AggregateType.None)
            {
                cmd = BuildStandardSelect(template);
            }
            else
            {
                cmd = BuildAggregateSelect(template);
            }

            return cmd;
        }

        private IDataCommand BuildStandardSelect(QueryTemplate template)
        {
            var builder = new StringBuilder();
            var cmd = new StormCommand(_provider);

            _parameterCount = 0;

            GenerateSelect(builder, template);
            GenerateFrom(builder, template);
            GenerateJoins(builder, template);
            GenerateCriteria(builder, template, cmd);
            GenerateOrderBy(builder, template);

            cmd.CommandText = builder.ToString();

            return cmd;
        }

        private IDataCommand BuildAggregateSelect(QueryTemplate template)
        {
            var builder = new StringBuilder();
            var cmd = new StormCommand(_provider);

            _parameterCount = 0;

            GenerateAggregate(builder, template);
            GenerateFrom(builder, template);
            GenerateJoins(builder, template);
            GenerateCriteria(builder, template, cmd);

            cmd.CommandText = builder.ToString();

            return cmd;
        }

        private IDataCommand BuildInsert(QueryTemplate template)
        {
            var builder = new StringBuilder();
            var cmd = new StormCommand(_provider);

            _parameterCount = 0;

            GenerateInsert(builder, template);
            GenerateInsertValues(builder, template, cmd);

            if (template.SelectIdentity)
            {
                builder.Append(";");
                GenerateSelectIdentity(builder);
            }

            cmd.CommandText = builder.ToString();

            return cmd;
        }

        private IDataCommand BuildUpdate(QueryTemplate template)
        {
            var builder = new StringBuilder();
            var cmd = new StormCommand(_provider);

            _parameterCount = 0;

            GenerateUpdate(builder, template);
            GenerateUpdateValues(builder, template, cmd);
            GenerateCriteria(builder, template, cmd);

            cmd.CommandText = builder.ToString();

            return cmd;
        }

        private IDataCommand BuildDelete(QueryTemplate template)
        {
            var builder = new StringBuilder();
            var cmd = new StormCommand(_provider);

            _parameterCount = 0;

            GenerateDelete(builder, template);
            GenerateCriteria(builder, template, cmd);

            cmd.CommandText = builder.ToString();

            return cmd;
        }

        private void GenerateSelect(StringBuilder builder, QueryTemplate template)
        {
            // Setup the select clause.
            if (template.Limit == 0)
            {
                builder.Append("SELECT ");
            }
            else
            {
                builder.AppendFormat("SELECT TOP {0} ", template.Limit);
            }

            // Add the columns to select.
            if (string.IsNullOrEmpty(template.SelectColumns))
            {
                builder.Append("* ");
            }
            else
            {
                builder.Append(template.SelectColumns + " ");
            }
        }

        private void GenerateAggregate(StringBuilder builder, QueryTemplate template)
        {
            var aggregate = FormatAggregate(template.AggregateType);

            builder.AppendFormat("SELECT {0}({1}) AS Value ", aggregate, template.AggregateColumn);
        }

        private void GenerateInsert(StringBuilder builder, QueryTemplate template)
        {
            builder.AppendFormat("INSERT INTO {0} ", template.TableName);

            builder.Append("(");
            bool first = true;
            foreach (var valueItem in template.InsertValues)
            {
                if (!first) builder.Append(",");

                builder.Append(valueItem.ColumnName);

                first = false;
            }
            builder.Append(") ");
        }

        private void GenerateInsertValues(StringBuilder builder, QueryTemplate template, IDataCommand cmd)
        {
            builder.Append("VALUES (");

            bool first = true;
            foreach (var valueItem in template.InsertValues)
            {
                if (!first) builder.Append(",");

                var paramName = "@" + valueItem.ColumnName;
                builder.Append(paramName);
                cmd.AddParameter(paramName, valueItem.Value);

                first = false;
            }

            builder.Append(") ");
        }

        private void GenerateSelectIdentity(StringBuilder builder)
        {
            builder.Append("SELECT SCOPE_IDENTITY() AS Id;");
        }

        private void GenerateUpdate(StringBuilder builder, QueryTemplate template)
        {
            builder.AppendFormat("UPDATE {0} SET ", template.TableName);
        }

        private void GenerateDelete(StringBuilder builder, QueryTemplate template)
        {
            builder.AppendFormat("DELETE FROM {0} ", template.TableName);
        }

        private void GenerateUpdateValues(StringBuilder builder, QueryTemplate template, IDataCommand cmd)
        {
            bool first = true;
            foreach (var valueItem in template.UpdateValues)
            {
                if (!first) builder.Append(",");

                var paramName = "@" + valueItem.ColumnName;
                builder.AppendFormat("{0} = {1}", valueItem.ColumnName, paramName);
                cmd.AddParameter(paramName, valueItem.Value);

                first = false;
            }
            builder.Append(" ");
        }

        private void GenerateFrom(StringBuilder builder, QueryTemplate template)
        {
            // Add the from clause.
            if (string.IsNullOrEmpty(template.TableAlias))
            {
                builder.AppendFormat("FROM {0} ", template.TableName);
            }
            else
            {
                builder.AppendFormat("FROM {0} AS {1} ", template.TableName, template.TableAlias);
            }
        }

        private void GenerateJoins(StringBuilder builder, QueryTemplate template)
        {
            // Add the joins.
            foreach (var joinItem in template.Joins)
            {
                var joinTypeName = FormatJoinType(joinItem.JoinType);
                if (string.IsNullOrEmpty(joinItem.AliasName))
                {
                    builder.AppendFormat("{0} JOIN {1} ON {2} = {3} ", joinTypeName, joinItem.TableName, joinItem.LeftColumn, joinItem.RightColumn);
                }
                else
                {
                    builder.AppendFormat("{0} JOIN {1} AS {2} ON {3} = {4} ", joinTypeName, joinItem.TableName, joinItem.AliasName, joinItem.LeftColumn, joinItem.RightColumn);
                }
            }
        }

        private void GenerateCriteria(StringBuilder builder, QueryTemplate template, IDataCommand cmd)
        {
            // Add the criteria.
            var first = true;
            foreach (var criteriaItem in template.Criteria)
            {
                var whereClause = "WHERE";
                if (!first)
                {
                    whereClause = FormatConstraintType(criteriaItem.CriteriaType);
                }
                var comparisonOperator = FormatComparisonOperator(criteriaItem.Comparison);

                if (criteriaItem.Comparison == ComparisonOperator.IsNull || criteriaItem.Comparison == ComparisonOperator.IsNotNull)
                {
                    builder.AppendFormat("{0} {1} {2} ", whereClause, criteriaItem.ColumnName, comparisonOperator);
                }
                else
                {
                    var paramNames = SetupParameters(cmd, criteriaItem);
                    builder.AppendFormat("{0} {1} {2} {3} ", whereClause, criteriaItem.ColumnName, comparisonOperator, paramNames);
                }

                first = false;
            }
        }

        private void GenerateOrderBy(StringBuilder builder, QueryTemplate template)
        {
            if (template.OrderBy.Count > 0)
            {
                builder.Append("ORDER BY ");
                bool first = true;
                foreach (var orderItem in template.OrderBy)
                {
                    if (!first) builder.Append(",");

                    var direction = orderItem.Ascending ? "ASC" : "DESC";
                    builder.AppendFormat("{0} {1}", orderItem.ColumnName, direction);

                    first = false;
                }
            }
        }

        private string SetupParameters(IDataCommand cmd, Criteria criteriaItem)
        {
            var paramNames = "";

            switch (criteriaItem.Comparison)
            {
                case ComparisonOperator.Equals:
                case ComparisonOperator.NotEquals:
                case ComparisonOperator.GreaterThan:
                case ComparisonOperator.GreaterThanEquals:
                case ComparisonOperator.LessThan:
                case ComparisonOperator.LessThanEquals:
                case ComparisonOperator.Like:
                case ComparisonOperator.NotLike:
                    paramNames = AddParameter(cmd, criteriaItem.ValueList[0]);
                    break;
                case ComparisonOperator.In:
                case ComparisonOperator.NotIn:
                    var nameList = new List<string>();
                    foreach (var valueItem in criteriaItem.ValueList)
                    {
                        var name = AddParameter(cmd, valueItem);
                        nameList.Add(name);
                    }

                    paramNames = "(" + string.Join(",", nameList.ToArray()) + ")";

                    break;
                case ComparisonOperator.Between:
                    var startName = AddParameter(cmd, criteriaItem.ValueList[0]);
                    var endName = AddParameter(cmd, criteriaItem.ValueList[1]);
                    paramNames = startName + " AND " + endName;
                    break;
            }

            return paramNames;
        }

        private string AddParameter(IDataCommand cmd, object value)
        {
            var paramName = string.Format("@p{0}", _parameterCount);
            _parameterCount++;

            cmd.AddParameter(paramName, value);

            return paramName;
        }

        private string FormatJoinType(JoinType value)
        {
            switch (value)
            {
                case JoinType.Inner:
                    return "INNER";
                case JoinType.LeftOuter:
                    return "LEFT OUTER";
                case JoinType.RightOuter:
                    return "RIGHT OUTER";
                default:
                    return "INNER";
            }
        }

        private object FormatComparisonOperator(ComparisonOperator value)
        {
            switch (value)
            {
                case ComparisonOperator.Equals:
                    return "=";
                case ComparisonOperator.NotEquals:
                    return "<>";
                case ComparisonOperator.GreaterThan:
                    return ">";
                case ComparisonOperator.GreaterThanEquals:
                    return ">=";
                case ComparisonOperator.LessThan:
                    return "<";
                case ComparisonOperator.LessThanEquals:
                    return "<=";
                case ComparisonOperator.Between:
                    return "BETWEEN";
                case ComparisonOperator.In:
                    return "IN";
                case ComparisonOperator.NotIn:
                    return "NOT IN";
                case ComparisonOperator.Like:
                    return "LIKE";
                case ComparisonOperator.NotLike:
                    return "NOT LIKE";
                case ComparisonOperator.IsNull:
                    return "IS NULL";
                case ComparisonOperator.IsNotNull:
                    return "IS NOT NULL";
                default:
                    return "=";
            }
        }

        private string FormatConstraintType(CriteriaType value)
        {
            switch (value)
            {
                case CriteriaType.And:
                    return "AND";
                case CriteriaType.Or:
                    return "OR";
                default:
                    return "AND";
            }
        }

        private string FormatAggregate(AggregateType value)
        {
            switch (value)
            {
                case AggregateType.Count:
                    return "COUNT";
                case AggregateType.Min:
                    return "MIN";
                case AggregateType.Max:
                    return "MAX";
                case AggregateType.Avg:
                    return "AVG";
                case AggregateType.Sum:
                    return "SUM";
                default:
                    return "";
            }
        }
    }
}