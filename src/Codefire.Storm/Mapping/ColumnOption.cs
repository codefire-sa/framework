using System;

namespace Codefire.Storm.Mapping
{
    [Flags]
    public enum ColumnOptions
    {
        None = 0,
        Insert = 1,
        Update = 2,
        CreateUser = 4,
        CreateDate = 8,
        ModifyUser = 16,
        ModifyDate = 32,
        SoftDelete = 64
    }
}