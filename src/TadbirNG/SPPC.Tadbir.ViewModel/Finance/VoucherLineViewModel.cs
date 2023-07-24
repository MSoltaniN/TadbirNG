using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class VoucherLineViewModel : ViewModelBase, IFiscalEntity
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
        /// نام شعبه سازمانی که اطلاعات مالی آن توسط این آرتیکل تحت تاثیر قرار می گیرد
        /// </summary>
        public string BranchName { get; set; }

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

        /// <summary>
        /// شناسه دیتابیسی منابع و مصارف مرتبط با این آرتیکل 
        /// </summary>
        public int? SourceAppId { get; set; }
    }
}
