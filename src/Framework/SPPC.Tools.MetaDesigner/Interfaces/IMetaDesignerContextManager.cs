using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface IMetaDesignerContextManager
    {
        IRepositoryModel Model { get; set; }
        IRepositoryView View { get; }
    }
}
