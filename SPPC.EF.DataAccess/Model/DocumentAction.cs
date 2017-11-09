using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class DocumentAction
    {
        public DocumentAction()
        {
            RequisitionVoucherLine = new HashSet<RequisitionVoucherLine>();
        }

        public int ActionId { get; set; }
        public int DocumentId { get; set; }
        public int? LineId { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public int? ConfirmedById { get; set; }
        public int? ApprovedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid Rowguid { get; set; }

        public User ApprovedBy { get; set; }
        public User ConfirmedBy { get; set; }
        public User CreatedBy { get; set; }
        public Document Document { get; set; }
        public User ModifiedBy { get; set; }
        public ICollection<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
    }
}
