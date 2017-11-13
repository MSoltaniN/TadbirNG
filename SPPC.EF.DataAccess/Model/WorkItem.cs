using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class WorkItem
    {
        public WorkItem()
        {
            WorkItemDocument = new HashSet<WorkItemDocument>();
        }

        public int WorkItemId { get; set; }
        public int CreatedById { get; set; }
        public int? TargetId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public string DocumentType { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public User CreatedBy { get; set; }
        public Role Target { get; set; }
        public ICollection<WorkItemDocument> WorkItemDocument { get; set; }
    }
}
