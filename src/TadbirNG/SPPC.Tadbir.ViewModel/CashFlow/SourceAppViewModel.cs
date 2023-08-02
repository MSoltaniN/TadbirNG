using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class SourceAppViewModel : IFiscalEntity, IBaseEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این منبع یا مصرف در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این منبع یا مصرف در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه ای منبع یا مصرف در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// نام دوره مالی که منبع یا مصرف در آن تعریف شده است
        /// </summary>
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// نام نوع مفهومی منبع یا مصرف
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیر فعال برای این سطر اطلاعاتی
        /// </summary>
        public string State { get; set; }
    }
}
