using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Inventory
{
    public partial class ProductInventoryViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی کالایی که موجودی برای آن تعریف شده
        /// </summary>
        [Display(Name = FieldNames.ProductField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? ProductId { get; set; }

        /// <summary>
        /// کد کالایی که موجودی برای آن تعریف شده
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// نام کالایی که موجودی برای آن تعریف شده
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی واحد اندازه گیری کالا برای این موجودی
        /// </summary>
        [Display(Name = FieldNames.UomField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? UomId { get; set; }

        /// <summary>
        /// نام واحد اندازه گیری کالا برای این موجودی
        /// </summary>
        public string UomName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی انباری که موجودی کالا برای آن تعریف شده
        /// </summary>
        [Display(Name = FieldNames.WarehouseField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? WarehouseId { get; set; }

        /// <summary>
        /// نام انباری که موجودی کالا برای آن تعریف شده
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این موجودی کالا در آن تعریف شده
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این موجودی کالا در آن تعریف شده
        /// </summary>
        public int BranchId { get; set; }
    }
}
