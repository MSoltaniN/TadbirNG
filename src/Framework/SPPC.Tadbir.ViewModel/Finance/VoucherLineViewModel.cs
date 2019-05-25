using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class VoucherLineViewModel : IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی سند مالی که این آرتیکل برای آن ایجاد شده است
        /// </summary>
        public int VoucherId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که اطلاعات مالی آن توسط این آرتیکل تحت تاثیر قرار می گیرد
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که وضعیت مالی آن توسط این آرتیکل تحت تاثیر قرار می گیرد
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// بردار حساب مورد استفاده در این آرتیکل
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پول یا ارز مورد استفاده برای مبلغ بدهکار یا بستانکار این آرتیکل
        /// </summary>
        [Display(Name = FieldNames.CurrencyTypeField)]
        public int? CurrencyId { get; set; }

        /// <summary>
        /// نام ارز به کار رفته در آرتیکل سند
        /// </summary>
        public string CurrencyName { get; set; }
    }
}
