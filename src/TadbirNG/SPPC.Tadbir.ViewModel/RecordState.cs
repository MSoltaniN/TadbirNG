using System;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// داده شمارشی برای وضعیت یک رکورد در دیتابیس
    /// </summary>
    public enum RecordState
    {
        /// <summary>
        /// وضعیت بدون تغییر برای رکورد
        /// </summary>
        Unmodified = 0,

        /// <summary>
        /// وضعیت ایجادشده برای رکورد
        /// </summary>
        Added = 1,

        /// <summary>
        /// وضعیت اصلاح شده برای رکورد
        /// </summary>
        Edited = 2,

        /// <summary>
        /// وضعیت حذف شده برای رکورد
        /// </summary>
        Deleted = 3
    }
}
