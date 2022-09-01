using System;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class UsedWidgetParameter
    {
        /// <summary>
        /// شناسه دیتابیسی ویجتی که این پارامتر برای نمایش آن در داشبورد مورد نیاز است
        /// </summary>
        public virtual int WidgetId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی یکی از پارامترهای ثابت که برای نمایش ویجت مورد نیاز است
        /// </summary>
        public virtual int ParameterId { get; set; }
    }
}
