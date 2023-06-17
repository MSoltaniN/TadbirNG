using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class VoucherViewModel : ViewModelBase, IFiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی این سند مالی
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// نام وضعیت ثبتی این سند مالی
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// جمع مقادیر بدهکار در آرتیکل های سند
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مقادیر بستانکار در آرتیکل های سند
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// مشخص می کند که آیا سند مورد نظر تایید شده است یا نه؟
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// مشخص می کند که آیا سند مورد نظر تصویب شده است یا نه؟
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مأخذ سند مالی
        /// </summary>
        public int OriginId { get; set; }

        /// <summary>
        /// نام مأخذ سند مالی
        /// </summary>
        public string OriginName { get; set; }

        /// <summary>
        /// نام نوع مفهومی سند مالی
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// مشخص می کند که شماره سندی بعد از این سند وجود دارد یا نه
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// مشخص می کند که شماره سندی قبل از این سند وجود دارد یا نه
        /// </summary>
        public bool HasPrevious { get; set; }
    }
}
