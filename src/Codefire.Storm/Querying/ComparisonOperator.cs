using System;

namespace Codefire.Storm.Querying
{
    /// <summary>
    /// 
    /// </summary>
    public enum ComparisonOperator
    {
        None = 0,
        Equals = 1,
        NotEquals = 2,
        GreaterThan = 3,
        GreaterThanEquals = 4,
        LessThan = 5,
        LessThanEquals = 6,
        Between = 7,
        In = 8,
        NotIn = 9,
        Like = 10,
        NotLike = 11,
        StartsWith = 12,
        EndsWith = 13,
        Contains = 14,
        IsNull = 15,
        IsNotNull = 16
    }
}