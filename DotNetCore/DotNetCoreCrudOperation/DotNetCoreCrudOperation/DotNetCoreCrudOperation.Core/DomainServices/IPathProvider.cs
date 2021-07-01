using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    public interface IPathProvider
    {
        string MapPath(string path);
    }
}
