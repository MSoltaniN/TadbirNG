using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class WidgetViewModel
    {
        /// <summary>
        /// نام و نام خانوادگی کاربر ایجادکننده این ویجت
        /// </summary>
        public string CreatedByFullName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تابع محاسباتی استفاده شده در این ویجت
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع انتخاب شده برای ویجت
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// نام چندزبانه تابع محاسباتی استفاده شده در این ویجت
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// نام چندزبانه نوع انتخاب شده برای ویجت
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// مجموعه ای از بردارهای حساب که به ویجت اضافه شده اند
        /// </summary>
        public List<FullAccountViewModel> Accounts { get; }
    }
}
