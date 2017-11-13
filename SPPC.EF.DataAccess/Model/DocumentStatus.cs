using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class DocumentStatus
    {
        public DocumentStatus()
        {
            Document = new HashSet<Document>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Document> Document { get; set; }
    }
}
