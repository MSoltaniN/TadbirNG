namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات یک سرفصل حسابداری پیش فرض برای وقتی که در پیکربندی سیستم ار کدینگ پیش فرض استفاده میشود
    /// </summary>
    public class DefaultAccountViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب والد این حساب در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی گروه مرتبط با حساب کل
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// محدوده دسترسی به حساب را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        public int BranchScope { get; set; }

        /// <summary>
        /// کد شناسایی برای سطح جاری سرفصل حسابداری در ساختار درختی
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// کد شناسایی کامل سرفصل حسابداری متشکل از کدهای تمام سطوح قبلی در ساختار درختی
        /// </summary>
        public string FullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// شماره سطح که عمق این سرفصل حسابداری را در ساختار درختی مشخص می کند
        /// </summary>
        public short Level { get; set; }

        /// <summary>
        /// مشخص می کند که آیا حساب مورد نظر فعال است یا نه؟
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// مشخص می کند که آیا تسعیر ارزی برای حساب مورد نظر قابل انجام است یا نه؟
        /// </summary>
        public bool IsCurrencyAdjustable { get; set; }

        /// <summary>
        /// محدودیت ثبت های مالی حساب را مشخص می کند
        /// </summary>
        public short TurnoverMode { get; set; }

        /// <summary>
        /// کد دورقمی برای حسابهای پیش فرض در صورتی که طول کد دو رقم انتخاب شده باشد
        /// </summary>
        public string TwoDigitCode { get; set; }
    }
}
