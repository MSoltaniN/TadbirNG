using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class WorkItemDocument
    {
        public int DocumentItemId { get; set; }
        public int? WorkItemId { get; set; }
        public int EntityId { get; set; }
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Document Document { get; set; }
        public WorkItem WorkItem { get; set; }
    }
}
