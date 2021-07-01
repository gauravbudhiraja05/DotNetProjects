using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories
{
    public enum SqlCommandType
    {
        Text=1,
        StoredProcedure=2,
        View=3
    }
}
