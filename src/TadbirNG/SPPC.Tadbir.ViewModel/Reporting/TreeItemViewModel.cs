using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات نمایشی مورد نیاز را برای یکی از شاخه های ساختار درختی گزارشات نگهداری میکند
    /// </summary>
    public class TreeItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی گزارش ذخیره شده
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شاخه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// عنوان محلی شده گزارش به صورتی که در نمای درختی گزارشات دیده می شود
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// آدرس وب مورد نیاز برای خواندن اطلاعات گزارش از سرویس وب
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// مشخص می کند که آیا شاخه مورد نظر مربوط به یک گزارش قابل اجرا است یا گروه بندی تعدادی از گزارشات
        /// </summary>
        public bool IsGroup { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش انتخاب شده گزارش سیستمی است یا گزارش ذخیره شده کاربری
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر به عنوان پیش فرض تعیین شده یا نه
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر طراحی شده است یا فوری
        /// </summary>
        public bool IsDynamic { get; set; }
    }
}
