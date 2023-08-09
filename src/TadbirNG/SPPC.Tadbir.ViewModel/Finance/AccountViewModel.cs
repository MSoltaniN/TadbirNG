using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class AccountViewModel : ViewModelBase, IFiscalEntity, ITreeEntityView, IBaseEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی حساب والد این حساب در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی گروه مرتبط با حساب کل
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی که این سرفصل حسابداری در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پول یا ارز مورد استفاده در این سرفصل حسابداری
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// تعداد حساب های زیرمجموعه این حساب در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// وضعیت فعال یا غیر فعال برای این سطر اطلاعاتی
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// یک رشته متنی برای این آبجکت برمی گرداند
        /// </summary>
        /// <returns>یک رشته متنی برای این آبجکت</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
