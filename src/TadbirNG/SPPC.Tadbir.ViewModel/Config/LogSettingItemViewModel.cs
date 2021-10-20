using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Config
{
    /// <summary>
    /// اطلاعاتی تنظیمات لاگ را برای یکی از عملیات نگهعداری می کند
    /// </summary>
    public class LogSettingItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی رکورد تنظیمات
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه دیتابیسی عملیات
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// نام محلی شده برای عملیات
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// مشخص می کند که آیا لاگ برای عملیات مورد نظر ایجاد می شود یه نه؟
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
