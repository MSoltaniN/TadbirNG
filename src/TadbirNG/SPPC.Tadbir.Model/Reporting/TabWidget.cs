using System;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class TabWidget
    {
        /// <summary>
        /// شناسه دیتابیسی برگه ای که ویجت مورد نظر به آن اضافه شده است
        /// </summary>
        public virtual int TabId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ویجتی که به برگه مورد نظر در یکی از داشبوردهای موجود اضافه شده است
        /// </summary>
        public virtual int WidgetId { get; set; }
    }
}
