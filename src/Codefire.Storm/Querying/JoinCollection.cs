using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class JoinCollection : List<Join>
    {
        public JoinCollection()
            : base()
        {
        }

        public Join Add(JoinType joinType, string tableName, string aliasName, string leftColumn, string rightColumn)
        {
            var item = new Join();
            item.JoinType = joinType;
            item.TableName = tableName;
            item.AliasName = aliasName;
            item.LeftColumn = leftColumn;
            item.RightColumn = rightColumn;

            Add(item);

            return item;
        }
    }
}