using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    public class SystemRepository : ISystemRepository
    {
        public ISecureRepository Repository { get; }

        public IMetadataRepository Metadata { get; }

        public IConfigRepository Config { get; }

        public IOperationLogRepository Logger { get; }
    }
}
