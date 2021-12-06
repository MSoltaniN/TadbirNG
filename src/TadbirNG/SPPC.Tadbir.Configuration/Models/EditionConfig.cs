using System;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات ویرایش جاری برنامه را نگهداری می کند
    /// </summary>
    public class EditionConfig
    {
        /// <summary>
        /// نام این ویرایش از برنامه
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// حداکثر تعداد مجاز برای کاربران همزمان
        /// </summary>
        public int MaxUsers { get; set; }

        /// <summary>
        /// حداکثر تعداد مجاز شرکت
        /// </summary>
        public int MaxCompanies { get; set; }

        /// <summary>
        /// حداکثر تعداد مجاز شعبه
        /// </summary>
        public int MaxBranches { get; set; }

        /// <summary>
        /// حداکثر عمق مجاز برای ساختار درختی شعبه
        /// </summary>
        public int MaxBranchDepth { get; set; }

        /// <summary>
        /// حداکثر عمق مجاز برای ساختار درختی سرفصل های حسابداری
        /// </summary>
        public int MaxAccountDepth { get; set; }

        /// <summary>
        /// حداکثر عمق مجاز برای ساختار درختی تفصیلی های شناور
        /// </summary>
        public int MaxDetailAccountDepth { get; set; }

        /// <summary>
        /// حداکثر عمق مجاز برای ساختار درختی مراکز هزینه
        /// </summary>
        public int MaxCostCenterDepth { get; set; }

        /// <summary>
        /// حداکثر عمق مجاز برای ساختار درختی پروژه ها
        /// </summary>
        public int MaxProjectDepth { get; set; }

        /// <summary>
        /// مشخص می کند که فیلتر شعبه در فرم ها و گزارش های مرتبط فعال است یا نه
        /// </summary>
        public bool EnableBranchFilter { get; set; }

        /// <summary>
        /// مشخص می کند که گزارشگیری به تفکیک شعبه فعال است یا نه
        /// </summary>
        public bool EnableByBranchReporting { get; set; }

        /// <summary>
        /// مشخص می کند که امکان مدیریت و اعمال فیلترهای سطری فعال است یا نه
        /// </summary>
        public bool EnableRowPermissions { get; set; }
    }
}
