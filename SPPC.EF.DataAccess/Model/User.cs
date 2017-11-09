using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class User
    {
        public User()
        {
            DocumentActionApprovedBy = new HashSet<DocumentAction>();
            DocumentActionConfirmedBy = new HashSet<DocumentAction>();
            DocumentActionCreatedBy = new HashSet<DocumentAction>();
            DocumentActionModifiedBy = new HashSet<DocumentAction>();
            Person = new HashSet<Person>();
            UserRole = new HashSet<UserRole>();
            WorkItem = new HashSet<WorkItem>();
            WorkItemHistory = new HashSet<WorkItemHistory>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsEnabled { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<DocumentAction> DocumentActionApprovedBy { get; set; }
        public ICollection<DocumentAction> DocumentActionConfirmedBy { get; set; }
        public ICollection<DocumentAction> DocumentActionCreatedBy { get; set; }
        public ICollection<DocumentAction> DocumentActionModifiedBy { get; set; }
        public ICollection<Person> Person { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
        public ICollection<WorkItem> WorkItem { get; set; }
        public ICollection<WorkItemHistory> WorkItemHistory { get; set; }
    }
}
