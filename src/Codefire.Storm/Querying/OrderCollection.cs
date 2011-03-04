using System;
using System.Collections.Generic;

namespace Codefire.Storm.Querying
{
    public class OrderCollection : List<Order>
    {
        public OrderCollection()
            : base()
        {
        }

        public Order Add(string memberName, string columnName, bool ascending)
        {
            var orderItem = new Order();
            orderItem.MemberName = memberName;
            orderItem.ColumnName = columnName;
            orderItem.Ascending = ascending;

            Add(orderItem);

            return orderItem;
        }
    }
}