using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Core
{
    public partial class OperationLog
    {
        public int OperationId { get; set; }

        public int? EntityTypeId { get; set; }

        public int? SourceId { get; set; }
    }
}
