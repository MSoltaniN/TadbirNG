using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Core
{
    public partial class SysOperationLog
    {
        public int OperationId { get; set; }

        public int? EntityTypeId { get; set; }

        public int? SourceId { get; set; }

        public int? SourceListId { get; set; }

        public int CompanyId { get; set; }

        public int UserId { get; set; }
    }
}
