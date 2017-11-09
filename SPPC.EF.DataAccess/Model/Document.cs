﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Document
    {
        public Document()
        {
            DocumentAction = new HashSet<DocumentAction>();
            Invoice = new HashSet<Invoice>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
            Transaction = new HashSet<Transaction>();
            WorkItemDocument = new HashSet<WorkItemDocument>();
            WorkItemHistory = new HashSet<WorkItemHistory>();
        }

        public int DocumentId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string EntityNo { get; set; }
        public string No { get; set; }
        public string OperationalStatus { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public DocumentStatus Status { get; set; }
        public DocumentType Type { get; set; }
        public ICollection<DocumentAction> DocumentAction { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<WorkItemDocument> WorkItemDocument { get; set; }
        public ICollection<WorkItemHistory> WorkItemHistory { get; set; }
    }
}
