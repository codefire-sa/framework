using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codefire.Storm.Querying
{
    public interface ISqlQuery
    {
        IDataProvider Provider { get; }
        QueryTemplate Template { get; }

        IDataCommand Build();
    }
}
