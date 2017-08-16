using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public partial class RequisitionVoucherLineViewModel
    {
        public int VoucherId { get; set; }

        [Display(Name = FieldNames.WarehouseField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int WarehouseId { get; set; }

        [Display(Name = FieldNames.ProductNameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int ProductId { get; set; }

        [Display(Name = FieldNames.UomField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int UomId { get; set; }

        public FullAccountViewModel FullAccount { get; set; }

        public int FiscalPeriodId { get; set; }

        public int BranchId { get; set; }

        public int DocumentId { get; set; }

        public DocumentActionViewModel DocumentAction { get; set; }
    }
}
