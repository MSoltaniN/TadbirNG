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
        /// <summary>
        /// شناسه دیتابیسی درخواست کالا
        /// </summary>
        public int VoucherId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی انبار مورد استفاده در این سطر درخواست کالا
        /// </summary>
        [Display(Name = FieldNames.WarehouseField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int WarehouseId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کالای مورد استفاده در این سطر درخواست کالا
        /// </summary>
        [Display(Name = FieldNames.ProductNameField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int ProductId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی واحد اندازه گیری مورد استفاده در این سطر درخواست کالا
        /// </summary>
        [Display(Name = FieldNames.UomField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int UomId { get; set; }

        /// <summary>
        /// اطلاعات نمایشی بردار حساب مورد استفاده در این سطر درخواست کالا
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مستند مرتبط با درخواست کالا
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// اطلاعات نمایشی اقدامات انجام شده روی این سطر درخواست کالا
        /// </summary>
        public DocumentActionViewModel DocumentAction { get; set; }
    }
}
