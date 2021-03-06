﻿using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class CriteriaCollection : List<Criteria>
    {
        public CriteriaCollection()
            : base()
        {
        }

        public Criteria Add(CriteriaType criteriaType, string memberName, string columnName, ComparisonOperator comparison, object value)
        {
            return Add(criteriaType, memberName, columnName, comparison, new object[] { value });
        }

        public Criteria Add(CriteriaType criteriaType, string memberName, string columnName, ComparisonOperator comparison, object[] values)
        {
            var item = new Criteria();
            item.CriteriaType = criteriaType;
            item.MemberName = memberName;
            item.ColumnName = columnName;
            item.Comparison = comparison;
            item.ValueList = values;

            Add(item);

            return item;
        }
    }
}