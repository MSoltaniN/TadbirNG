using System;
using System.Collections.Generic;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Persistence
{
    public interface IRepositoryStorage
    {
        Storage Storage { get; set; }
        Repository Load();
        void Save(Repository repository);
    }
}
