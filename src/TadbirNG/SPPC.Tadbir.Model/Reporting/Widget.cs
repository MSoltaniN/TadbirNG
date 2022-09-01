using System;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class Widget
    {
        /// <summary>
        /// شناسه دیتابیسی تابع محاسباتی استفاده شده در این ویجت
        /// </summary>
        public virtual int FunctionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع انتخاب شده برای ویجت
        /// </summary>
        public virtual int TypeId { get; set; }
    }
}
