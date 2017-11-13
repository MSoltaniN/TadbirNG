using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class WorkItemHistory
    {
        public int HistoryItemId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int DocumentId { get; set; }
        public int EntityId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Remarks { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Document Document { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
