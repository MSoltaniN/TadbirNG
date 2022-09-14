using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    public partial class TabWidgetViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی برگه ای که ویجت به آن اضافه شده است
        /// </summary>
        public int TabId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public int WidgetId { get; set; }

        /// <summary>
        /// عنوان ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public string WidgetTitle { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تابع محاسباتی در ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public int WidgetFunctionId { get; set; }

        /// <summary>
        /// نام تابع محاسباتی در ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public string WidgetFunctionName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public int WidgetTypeId { get; set; }

        /// <summary>
        /// نوع ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public string WidgetTypeName { get; set; }

        /// <summary>
        /// شرح ویجت اضافه شده به برگه داشبورد
        /// </summary>
        public string WidgetDescription { get; set; }

        /// <summary>
        /// مجموعه ای از بردارهای حساب که به ویجت اضافه شده اند
        /// </summary>
        public List<FullAccountViewModel> WidgetAccounts { get; }

        /// <summary>
        /// مجموعه پارامترهای مورد نیاز برای نمایش گرافیکی ویجت
        /// </summary>
        public List<FunctionParameterViewModel> WidgetParameters { get; }
    }
}
