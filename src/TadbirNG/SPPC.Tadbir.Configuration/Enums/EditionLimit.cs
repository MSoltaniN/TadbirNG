using System;

namespace SPPC.Tadbir.Configuration.Enums
{
    /// <summary>
    /// داده شمارشی برای انواع محدودیت های تعریف شده برای ویرایش برنامه
    /// </summary>
    public enum EditionLimit
    {
        /// <summary>
        /// محدودیت نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// محدودیت تعداد شرکت های قابل ایجاد
        /// </summary>
        CompanyCount = 1,

        /// <summary>
        /// محدودیت تعداد شعبه های سازمانی قابل ایجاد و عمق ساختار درختی آنها
        /// </summary>
        BranchCountAndDepth = 2,

        /// <summary>
        /// محدودیت عمق ساختار درختی سرفصل های حسابداری
        /// </summary>
        AccountDepth = 3,

        /// <summary>
        /// محدودیت عمق ساختار درختی تفصیلی های شناور
        /// </summary>
        DetailAccountDepth = 4,

        /// <summary>
        /// محدودیت عمق ساختار درختی مراکز هزینه
        /// </summary>
        CostCenterDepth = 5,

        /// <summary>
        /// محدودیت عمق ساختار درختی پروژه ها
        /// </summary>
        ProjectDepth = 6,

        /// <summary>
        /// محدودیت تعریف شده برای امکان مدیریت دسترسی های سطری موجودیت ها
        /// </summary>
        RowPermissionAccess = 7
    }
}
