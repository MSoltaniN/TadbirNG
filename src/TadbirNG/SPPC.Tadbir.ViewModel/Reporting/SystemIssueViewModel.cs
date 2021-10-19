namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class SystemIssueViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی دستور والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دسترسی مورد نیاز
        /// </summary>
        public int? PermissionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی موجودیت
        /// </summary>
        public int? ViewId { get; set; }
    }
}
