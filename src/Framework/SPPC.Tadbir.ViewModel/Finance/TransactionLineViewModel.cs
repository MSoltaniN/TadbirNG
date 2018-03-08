using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class TransactionLineViewModel
    {
        /// <summary>
        /// Gets or sets the identifier of the transaction that contains this line (article) item
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the corporate branch in which this line (article) item is defined
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this line (article) item is defined
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.AccountField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.DetailAccountField)]
        public int? DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.CostCenterField)]
        public int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه از بردار حساب مورد استفاده در این آرتیکل مالی
        /// </summary>
        [Display(Name = FieldNames.ProjectField)]
        public int? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the currency that qualifies monetary values in this article.
        /// </summary>
        [Display(Name = FieldNames.CurrencyTypeField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? CurrencyId { get; set; }
    }
}
