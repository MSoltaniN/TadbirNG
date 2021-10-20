using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مورد استفاده در مدیریت ارتباطات بین مولفه های بردار حساب را نگهداری می کند
    /// </summary>
    public class RelationsConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RelationsConfig()
        {
            UseLeafDetails = true;
            UseLeafCostCenters = true;
            UseLeafProjects = true;
        }

        /// <summary>
        /// مشخص می کند که آیا ارتباطات فقط در آخرین سطح تفصیلی های شناور برقرار شوند یا نه.
        /// پیش فرض این تنظیم، مقدار "درست" است
        /// </summary>
        public bool UseLeafDetails { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ارتباطات فقط در آخرین سطح مراکز هزینه برقرار شوند یا نه.
        /// پیش فرض این تنظیم، مقدار "درست" است
        /// </summary>
        public bool UseLeafCostCenters { get; set; }

        /// <summary>
        /// مشخص می کند که آیا ارتباطات فقط در آخرین سطح پروژه ها برقرار شوند یا نه.
        /// پیش فرض این تنظیم، مقدار "درست" است
        /// </summary>
        public bool UseLeafProjects { get; set; }
    }
}
