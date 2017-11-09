using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Branch
    {
        public Branch()
        {
            Account = new HashSet<Account>();
            Invoice = new HashSet<Invoice>();
            InvoiceLine = new HashSet<InvoiceLine>();
            IssueReceiptVoucher = new HashSet<IssueReceiptVoucher>();
            IssueReceiptVoucherLine = new HashSet<IssueReceiptVoucherLine>();
            ProductInventory = new HashSet<ProductInventory>();
            RequisitionVoucher = new HashSet<RequisitionVoucher>();
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
            RoleBranch = new HashSet<RoleBranch>();
            Transaction = new HashSet<Transaction>();
            TransactionLine = new HashSet<TransactionLine>();
        }

        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Company Company { get; set; }
        public ICollection<Account> Account { get; set; }
        public ICollection<Invoice> Invoice { get; set; }
        public ICollection<InvoiceLine> InvoiceLine { get; set; }
        public ICollection<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public ICollection<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public ICollection<ProductInventory> ProductInventory { get; set; }
        public ICollection<RequisitionVoucher> RequisitionVoucher { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
        public ICollection<RoleBranch> RoleBranch { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
        public ICollection<TransactionLine> TransactionLine { get; set; }
    }
}
