using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.Procurement
{
    public partial class RequisitionVoucherViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی نوع درخواست کالا
        /// </summary>
        [Display(Name = FieldNames.RequisitionVoucherTypeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? TypeId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شخص درخواست کننده
        /// </summary>
        [Display(Name = FieldNames.RequesterField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? RequesterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شخص دریافت کننده
        /// </summary>
        [Display(Name = FieldNames.ReceiverField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? ReceiverId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی واحد درخواست کننده
        /// </summary>
        [Display(Name = FieldNames.RequesterUnitField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? RequesterUnitId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی واحد دریافت کننده
        /// </summary>
        [Display(Name = FieldNames.ReceiverUnitField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? ReceiverUnitId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی انبار مرتبط با این درخواست
        /// </summary>
        [Display(Name = FieldNames.WarehouseField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? WarehouseId { get; set; }

        /// <summary>
        /// اطلاعات نمایشی بردار حساب این درخواست کالا
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// اطلاعات نمایشی مستند مرتبط با این درخواست کالا
        /// </summary>
        public DocumentViewModel Document { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی
        /// </summary>
        public int BranchId { get; set; }
    }
}
