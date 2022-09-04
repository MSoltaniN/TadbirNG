using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class WidgetViewModel
    {
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
    }
}
