using System;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// مدل نمایشی برای یک سطر اطلاعاتی مرتبط با یک سطر اطلاعاتی اصلی
    /// </summary>
    public class RelatedItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی سطر اطلاعاتی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی رکورد والد این سطر اطلاعاتی مرتبط - برای موجودیت های مرتبط درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// نام یا عنوان سطر اطلاعاتی
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// مشخص می کند که سطر اطلاعاتی مورد نظر انتخاب شده یا نه
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
