using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    public interface ISystemRepository
    {
        ISecureRepository Repository { get; }

        IMetadataRepository Metadata { get; }

        IConfigRepository Config { get; }

        IOperationLogRepository Logger { get; }
    }
}
